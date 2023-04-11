using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.Clients
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterAnimator))]
    public class ClientMove: MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private List<Transform> _places;

        private int _indexPlace=0;
        private NavMeshAgent _navMesh;
       private Transform _target;
       private CharacterAnimator _animator;

       private void Start()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _animator = GetComponent<CharacterAnimator>();
            _navMesh.speed = _speed;
            SetTarget(_places[_indexPlace]);
        }

       private void LateUpdate()
       {
           _animator.MoveAnimation(_navMesh.velocity.magnitude / _navMesh.speed);
       }

       public void StopMove()
       {
           _navMesh.isStopped = true;
       }

       public void GetNextTarget()
       {
           _navMesh.isStopped = false;
           SetTarget(_places[_indexPlace]);
       }

       private void SetTarget(Transform newTarget)
       {
           _target = newTarget;
           _navMesh.destination = _target.position;
           _indexPlace++;
       }
    }
}