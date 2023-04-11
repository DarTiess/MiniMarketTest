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

        //   public void PushingToCashBox(ClientStack target, Store store)
        // {
        //     transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
        //          .OnComplete(() =>
        ////           {
        //                   target.StackIn();
        //          store.StackOut();
        //               });
        //  }
    }
}