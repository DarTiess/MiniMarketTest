using System;
using System.Collections.Generic;
using DefaultNamespace.Clients;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace.Cash
{
    public class CashTable: MonoBehaviour
    {
        [Header("Box Settings")]
        [SerializeField]
        private BoxInCashTable _box;
        [SerializeField]
        private float _jumpBoxDuration;
        [SerializeField] private float _jumpBoxForce;
        [Header("MoneySettings")]
        [SerializeField]
        private float _jumpMoneyDuration;
        [SerializeField]
        private float _jumpMoneyForce;
        [SerializeField]
        private List<MoneyInCashTable> _moneyList;
        private Queue<ClientStack> _clientsList = new Queue<ClientStack>();
        private int _indexMoney = 0;
    

        public Transform BoxPlace => _box.transform;
        public int ClientCount => _clientsList.Count;
        public Transform MoneyPlace => _moneyList[0].transform;
        public int MoneyCount => _indexMoney;

        private void Start()
        {
            OnStart();
        }

        private void OnStart()
        {
            _box.gameObject.SetActive(false);
            _box.InitMoney(_jumpBoxDuration, _jumpBoxForce);

            foreach (MoneyInCashTable moneyInCashTable in _moneyList)
            {
                moneyInCashTable.InitMoney(_jumpMoneyDuration, _jumpMoneyForce);
                moneyInCashTable.gameObject.SetActive(false);
            }
        }

        public void StayOnQueue(ClientStack clientStack)
        {
            _clientsList.Enqueue(clientStack);
        }

        public void CreateBox()
        {
            _clientsList.Dequeue().DisableStack(this);
          
        }
        
        public void StackIn(ClientStack client)
        {
           _box.gameObject.SetActive(true);
           _box.PushingToClient(client, this);
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

        public void StackOutMoney()
        {
            _moneyList[_indexMoney-1].gameObject.SetActive(false);
            _indexMoney--;
        }

        public void PayMoney(PlayerMoneyStack playerMoneyStack)
        {
            _moneyList[_indexMoney-1].PushingToPlayer(playerMoneyStack, this);
            
        }
    }
}