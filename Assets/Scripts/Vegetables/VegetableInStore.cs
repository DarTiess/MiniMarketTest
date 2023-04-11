using DefaultNamespace.Clients;
using DefaultNamespace.Store;
using DG.Tweening;
using UnityEngine;

namespace Vegetables
{
    public class VegetableInStore : MonoBehaviour
    {
        private float _speed;
        private float _jumpForce;
        private Vector3 _startPosition;

        public void InitVegetable(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
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