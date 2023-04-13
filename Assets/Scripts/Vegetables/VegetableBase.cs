using UnityEngine;

namespace Vegetables
{
    public abstract class VegetableBase : MonoBehaviour
    {
        protected float _speed;
        protected float _jumpForce;

        public virtual void Init(float speed, float jumpForce)
        {
            _speed = speed;
            _jumpForce = jumpForce;
        }
        
    }
}