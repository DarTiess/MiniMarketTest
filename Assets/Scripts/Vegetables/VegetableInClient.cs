using DefaultNamespace.Cash;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Clients
{
    public class VegetableInClient: MonoBehaviour
    {
        private float _speed;
        private float _jumpForce;
      

        public void InitVegetable(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
          
        }

           public void PushingToCashBox(CashTable target, ClientStack client)
        {
            transform.DOJump(target.BoxPlace.position, _jumpForce, 1, _speed)
                  .OnComplete(() =>
                  {
                    target.StackIn(client);
                    client.StackOut();
                  });
          }
          
    }
}