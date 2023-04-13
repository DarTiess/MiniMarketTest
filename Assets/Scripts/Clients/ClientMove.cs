using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.Clients
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterAnimator))]
    public class ClientMove: MonoBehaviour
    {
        
        private float _speed;
        private List<Transform> _places=new List<Transform>();

        private int _indexPlace=0;
        private NavMeshAgent _navMesh;
       private Transform _target;
       private CharacterAnimator _animator;
       private ClientsGenerator _parent;
       private ClientStack _clientStack;
      
       private void Start()
       {
           OnStart();
       }

       private void Update()
       {
           if (_indexPlace >= _places.Count)
           {
               if (Vector3.Distance(transform.position, _target.position) <= 1f)
               {
                   _parent.ClientIsGone(this);
                   _clientStack.ClearProgress();
               }
           }     
        }

       private void LateUpdate()
       {
           _animator.MoveAnimation(_navMesh.velocity.magnitude / _navMesh.speed);
       }

       public void Initialize(float speed, List<Transform> places, ClientsGenerator clientsGenerator)
       {
           _speed = speed;
           _parent = clientsGenerator;
          // InitialPath(places);
       }

       public void BeginPath(List<Transform> places)
       {
           InitialPath(places);
           OnStart();
           _indexPlace = 0;
           GetNextTarget();
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

       private void InitialPath(List<Transform> places)
       {
           _places.Clear();
           foreach (Transform place in places)
           {
               _places.Add(place);
           }
       }

       private void OnStart()
       {
           _navMesh = GetComponent<NavMeshAgent>();
           _animator = GetComponent<CharacterAnimator>();
           _clientStack = GetComponent<ClientStack>();
           _navMesh.speed = _speed;
       }

       private void SetTarget(Transform newTarget)
       {
           _target = newTarget;
           _navMesh.destination = _target.position;
           _indexPlace++;
           
           
       }
    }
}