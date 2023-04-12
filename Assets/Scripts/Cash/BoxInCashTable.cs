using DefaultNamespace.Clients;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Cash
{
    public class BoxInCashTable : MonoBehaviour
    {
        private float _speed;
        private float _jumpForce;

        public void InitMoney(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
        }

        public void PushingToClient(ClientStack target, CashTable cashTable)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.HadBox();
                         cashTable.DisableBox();
                     });
        }
    }
}