using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Cash;
using Garden;
using UnityEngine;
using Vegetables;

namespace DefaultNamespace.Clients
{
    [RequireComponent(typeof(ClientMove))]
    [RequireComponent(typeof(CharacterAnimator))]
    public class ClientStack: MonoBehaviour, IStack
    {
        public event Action IsFull;
        public event Action GetVegetable;
        public event Action IsEmpty;
        
        [SerializeField]
        private VegetableType _wishVegetableType;
        [SerializeField]
        private List<VegetableInClient> _vegetableList;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpForce;

        public int WisheVegetables => _vegetableList.Count;
        
        private ClientMove _clientMove;
        private int _vegIndex=0;
        private bool _stacking;
        private bool _full;
        private CharacterAnimator _animator;
        private bool _onCash;

        private void Start()
        {
            _animator = GetComponent<CharacterAnimator>();
            _clientMove = GetComponent<ClientMove>();
            HideStackList();
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Store"))
            {
                if (!_full)
                {
                    if (!_stacking)
                    {
                        Debug.Log("InStore");
                        _stacking = true;
                       StartCoroutine(TryGetVegetable(other.GetComponent<Store.Store>()));
                    }
                }
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Cash"))
            {
                if (_onCash)
                {
                    return;
                }
                Debug.Log("InCash");
                _onCash = true;
                StayOnCash(other.GetComponent<CashTable>());
            }
        }

        public void StackIn()
        {
            if (_full)
            {
                return;
            }
            GetVegetable?.Invoke();
            _animator.HasStack();
            _vegetableList[_vegIndex].gameObject.SetActive(true);
            _vegIndex++;
            _stacking = false;
            if (_vegIndex >= _vegetableList.Count)
            {
                _clientMove.GetNextTarget();
                _full = true;
                IsFull?.Invoke();
            }
        }

        public void StackOut()
        {
            _animator.StackEmpty();
            for(int i=_vegetableList.Count-1;i>=0;i--)
            {
                _vegetableList[i].gameObject.SetActive(false);
            }
            _clientMove.GetNextTarget();
            IsEmpty?.Invoke();
        }

        private void HideStackList()
        {
            foreach (VegetableInClient vegetable in _vegetableList)
            {
                vegetable.gameObject.SetActive(false);
                vegetable.InitVegetable(_jumpDuration, _jumpForce);
            }
        }

        private IEnumerator TryGetVegetable(Store.Store store)
        {
            bool isPulling = store.PullVegetableToClient(this);
            while (!isPulling)
            {
                isPulling = store.PullVegetableToClient(this);
                yield return null;
            }
        }

        private void StayOnCash(CashTable cashTable)
        {
            cashTable.StayOnQueue(this);
        }
    }
}