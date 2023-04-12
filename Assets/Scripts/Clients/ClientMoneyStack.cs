using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Cash
{
    public class ClientMoneyStack: MonoBehaviour
    {
        [SerializeField] private MoneyInClient _moneyPrefab;
        [SerializeField]
        private int _moneyCount;
        [SerializeField]
        private float _jumDuration;
        [SerializeField]
        private float _jumpForce;

        private int _indexMoney = 0;
        private List<MoneyInClient> _moneyList = new List<MoneyInClient>();

        private void Start()
        {
            CreateMoneyStack();
        }

        private void CreateMoneyStack()
        {
            for (int i = 0; i < _moneyCount; i++)
            {
                MoneyInClient money = Instantiate(_moneyPrefab, gameObject.transform);
                money.gameObject.SetActive(false);
                money.InitMoney(_jumDuration, _jumpForce);
                _moneyList.Add(money);
            }
        }

        public void PayMoney(CashTable target)
        {
            if (_indexMoney >= _moneyList.Count)
            {
                _indexMoney = 0;
            }
            _moneyList[_indexMoney].gameObject.transform.position = gameObject.transform.position;
            _moneyList[_indexMoney].gameObject.SetActive(true);
            _moneyList[_indexMoney].PushingToChashTable(target, this);
           
        }

        public void DisableMoney()
        {
            _moneyList[_indexMoney].gameObject.SetActive(false);
            _indexMoney++;
        }
    }
}