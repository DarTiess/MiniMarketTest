using DefaultNamespace.Clients;
using DefaultNamespace.Store;
using DG.Tweening;
using UnityEngine;

namespace Vegetables
{
    public class VegetableInStore : VegetableBase
    {
        private Vector3 _startPosition;

        public override void Init(float speed, float jumpForce)
        {
            base.Init(speed, jumpForce);
            _startPosition = transform.position;
        }

        public void PushingToClient(ClientStack target, Store store)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.StackIn();
                         store.StackOut();
                         SetToStartPosition();
                     });
        }
        private void SetToStartPosition()
        {
            transform.position = _startPosition; 
           
        }

    }
}