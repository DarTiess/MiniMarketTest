using DefaultNamespace.Store;
using DG.Tweening;
using UnityEngine;

namespace Vegetables
{
    public abstract class VegetableBase : MonoBehaviour
    {
        protected float _speed;
        protected float _jumpForce;

        public virtual void InitVegetable(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
        }

        public virtual void Pushing(IStack target, IStack player)
        {
          
        }
    }
}