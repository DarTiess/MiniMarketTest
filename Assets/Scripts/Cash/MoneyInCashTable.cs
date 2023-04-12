using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Cash
{
    public class MoneyInCashTable : MonoBehaviour
    {
        private float _speed;
        private float _jumpForce;
        private Vector3 _startPosition;

        public void InitMoney(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
            _startPosition = transform.position;
        }

        public void PushingToPlayer(PlayerMoneyStack target, CashTable cashTable)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                        target.StackIn();
                        cashTable.StackOutMoney();
                        SetToStartPosition();
                     });
        }
        private void SetToStartPosition()
        {
            transform.position = _startPosition; 
           
        }
        
    }
}