using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Cash
{
    public class MoneyInClient : MonoBehaviour
    {
        private float _speed;
        private float _jumpForce;

        public void InitMoney(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
        }

        public void PushingToChashTable(CashTable target, ClientMoneyStack clientStack)
        {
            transform.DOJump(target.MoneyPlace.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.StackMoneyIn();
                         clientStack.DisableMoney();
                     });
        }
    }
}