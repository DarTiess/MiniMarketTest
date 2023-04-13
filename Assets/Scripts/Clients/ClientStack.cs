using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Cash;
using UnityEngine;
using Vegetables;

namespace DefaultNamespace.Clients
{
    [RequireComponent(typeof(ClientMove))]
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(ClientMoneyStack))]
    public class ClientStack: MonoBehaviour, IStack
    {
        public event Action IsFull;
        public event Action GetVegetable;
        public event Action IsEmpty;
        public event Action OnRestart;
        
        [SerializeField] private VegetableType _wishVegetableType;
        [SerializeField] private List<Transform> _vegetablesPlaces;
        [SerializeField] private VegetableInClient _vegetablePrefab;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private GameObject _boxInHand;
        public int WisheVegetables =>  _vegetablesPlaces.Count;
        
        private ClientMove _clientMove;
        private int _vegIndex=0;
        private bool _stacking;
        private bool _full;
        private CharacterAnimator _animator;
        private bool _onCash;
        private ClientMoneyStack _clientMoney;
        private List<VegetableInClient> _vegetableList = new List<VegetableInClient>();

        private void Start()
        {
            _animator = GetComponent<CharacterAnimator>();
            _clientMove = GetComponent<ClientMove>();
            _clientMoney = GetComponent<ClientMoneyStack>();
            CreateStackList();
           HideBoxInHand();
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Store"))
            {
                if (!_full)
                {
                    if (!_stacking)
                    {
                        _stacking = true;
                       StartCoroutine(TryGetVegetable(other.GetComponent<Store.Store>()));
                    }
                }
            }
            if (other.CompareTag("Cash"))
            {
                if (_onCash)
                {
                    return;
                }
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
           
                _vegetableList[_vegIndex-1].gameObject.SetActive(false);
                _vegIndex--;
            _clientMove.GetNextTarget();
            IsEmpty?.Invoke();
        }

        public void DisableStack(CashTable target)
        {
            for(int i=_vegetableList.Count-1;i>=0;i--)
            {
                _vegetableList[i].PushingToCashBox(target, this);
                _clientMoney.PayMoney(target);

            }
        }

        public void HadBox()
        {
            _boxInHand.SetActive(true);
            _animator.HasStack();
        }

        public void ClearProgress()
        {
            _full = false;
            _stacking = false;
            _onCash = false;
           
            HideBoxInHand();
            ReplaceStackList();
            OnRestart?.Invoke();
        }

        private void CreateStackList()
        {
            for (int i = 0; i <  _vegetablesPlaces.Count; i++)
            {
                VegetableInClient pref = Instantiate(_vegetablePrefab, _vegetablesPlaces[i].position, _vegetablesPlaces[i].rotation);
                pref.Init(_jumpDuration, _jumpForce);
                
                pref.transform.parent = gameObject.transform;
               
                pref.gameObject.SetActive(false);
               _vegetableList.Add(pref);
            
            }
        }

        private void ReplaceStackList()
        {
            for (int i = 0; i < _vegetableList.Count; i++)
            {
                _vegetableList[i].transform.position = _vegetablesPlaces[i].transform.position;
            }
        }

        private void HideBoxInHand()
        {
            _boxInHand.SetActive(false);
        }

        private IEnumerator TryGetVegetable(Store.Store store)
        {
            _clientMove.StopMove();
            while (!store.NotEmpty)
            {
                yield return null;
            }
            store.PullVegetableToClient(this);
        }

        private void StayOnCash(CashTable cashTable)
        {
            cashTable.StayOnQueue(this);
            _clientMove.StopMove();
        }
    }
}