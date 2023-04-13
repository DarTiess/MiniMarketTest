using DG.Tweening;
using UnityEngine;
using Vegetables;

namespace DefaultNamespace.Cash
{
    public class MoneyInCashTable : VegetableBase
    {
        private Vector3 _startPosition;

        public override void Init(float speed, float jumpForce)
        {
            base.Init(speed, jumpForce);
            _startPosition = transform.position;
        }

        public void PushingToPlayer(PlayerMoneyStack target, CashTable cashTable)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     { 
                         target.StackIn();
                        SetToStartPosition();
                     });
        }
        private void SetToStartPosition()
        {
            transform.position = _startPosition; 
           
        }
        
    }
}