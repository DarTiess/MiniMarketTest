using System.Collections.Generic;
using DefaultNamespace.Clients;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Cash
{
    public class CashTable: MonoBehaviour
    {
        [Header("Box Settings")]
        [SerializeField] private BoxInCashTable _box;
        [SerializeField] private float _jumpBoxDuration;
        [SerializeField] private float _jumpBoxForce;
       
        [Header("MoneySettings")]
        [SerializeField] private float _jumpMoneyDuration;
        [SerializeField] private float _jumpMoneyForce;
        [SerializeField] private List<MoneyInCashTable> _moneyList;
      
        [Header("ClientsSettings")]
        [SerializeField] private List<Transform> _clientsPlaces;
        [SerializeField] private float _moveDuration;

        public Transform BoxPlace => _box.transform;
        public int ClientCount => _clientsList.Count;
        public Transform MoneyPlace => _moneyList[0].transform;
        public int MoneyCount => _indexMoney;

        private Queue<ClientStack> _clientsList = new Queue<ClientStack>();
        public int _indexMoney = 0;
        private int _indexPlaces=0;
        

        private void Start()
        {
            OnStart();
        }

        public void StayOnQueue(ClientStack clientStack)
        {
            if (_indexPlaces >= _clientsPlaces.Count)
            {
                _indexPlaces = 0;
            }
            _clientsList.Enqueue(clientStack);
            clientStack.gameObject.transform.DOMove(_clientsPlaces[_indexPlaces].position, _moveDuration)
                       .OnComplete(() =>
                       {
                           var look = Quaternion.LookRotation(new Vector3(transform.position.x, clientStack.transform.position.y,transform.position.z) - 
                          clientStack.transform.position);

                           clientStack.transform.DOLocalRotateQuaternion(look, 0.5f);
                          
                       });
            _indexPlaces++;
        }

        public void CreateBox(PlayerMoneyStack playerMoney)
        {
            _clientsList.Dequeue().DisableStack(this);
        }

        public void StackIn(ClientStack client)
        {
           _box.gameObject.SetActive(true);
           _box.Pushing(client, this);
        }

        public void StackMoneyIn()
        {
            _moneyList[_indexMoney].gameObject.SetActive(true);
            _indexMoney++;
        }

        public void DisableBox()
        {
            _box.gameObject.SetActive(false);
        }

        public void PayMoney(PlayerMoneyStack playerMoneyStack)
        {
            _moneyList[_indexMoney-1].PushingToPlayer(playerMoneyStack, this);
            StackOutMoney();
        }

        private void OnStart()
        {
            _box.gameObject.SetActive(false);
            _box.Init(_jumpBoxDuration, _jumpBoxForce);

            foreach (MoneyInCashTable moneyInCashTable in _moneyList)
            {
                moneyInCashTable.Init(_jumpMoneyDuration, _jumpMoneyForce);
                moneyInCashTable.gameObject.SetActive(false);
            }
        }

        private void StackOutMoney()
        {
            _moneyList[_indexMoney-1].gameObject.SetActive(false);
                                                                                   
            _indexMoney--;
        }
    }
}