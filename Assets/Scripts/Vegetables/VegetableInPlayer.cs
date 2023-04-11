using DefaultNamespace.Clients;
using DefaultNamespace.Store;
using DG.Tweening;
using UnityEngine;

namespace Vegetables
{
  
    public class VegetableInPlayer : MonoBehaviour
    {
        private float _speed;
        private float _jumpForce;

        public void InitVegetable(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
        }

        public void PushingFromPlayer(Store target, PlayersStack player)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.StackIn();
                         player.StackOut();
                     });
        }
    }
}