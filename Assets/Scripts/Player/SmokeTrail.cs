using UnityEngine;

namespace Player
{
    public class SmokeTrail: MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _delay;
        private Vector3 _startPosition;
        private Vector3 _endPosition;

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            _startPosition = transform.position;
            _endPosition = _target.position;

            transform.position = Vector3.Slerp(_startPosition, _endPosition,  Time.fixedDeltaTime * _delay);
            _startPosition = transform.position;
            _endPosition = _target.position;
           

        }
    }
}