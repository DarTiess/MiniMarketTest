using System.Collections.Generic;
using DefaultNamespace.BuyPlace;
using DefaultNamespace.Cash;
using UnityEngine;
using Zenject;

public class PlayerMoneyStack : MonoBehaviour
{
    [SerializeField] private MoneyInPlayer _moneyPrefab;
    [SerializeField] private int _moneyCount;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpForce;
    private Economics _economics;
    private bool _onBuy;
    private List<MoneyInPlayer> _moneyList = new List<MoneyInPlayer>();
    private int _indexMoney = 0;
    private int _paidMoney;
    private bool _takeMoney;
    private bool _boxing;

    [Inject]
    private void Construct(Economics economics)
    {
        _economics = economics;
    }

    private void Start()
    {
        CreateStack();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cash"))
        {
            CashTable cashTable = other.GetComponent<CashTable>();
            CheckClientsQueue(cashTable);

            CheckCashMoney(cashTable);
        }

        if (other.CompareTag("BuyPlace"))
        {
            if (_onBuy)
            {
                return;
            }

            BuyPlace buyPlace = other.GetComponent<BuyPlace>();
            CheckBank(buyPlace);

        }
    }


    public void StackIn()
  {
      _economics.GetMoney();
      _takeMoney = false;
      _boxing = false;
  }

    public void StackOut(MoneyInPlayer money)
    {
       money.gameObject.SetActive(false);
       _onBuy = false;
    }

    private void CreateStack()
    {
        for (int i = 0; i < _moneyCount; i++)
        {
            MoneyInPlayer money = Instantiate(_moneyPrefab, transform);
            money.gameObject.SetActive(false);
            money.Init(_jumpDuration, _jumpForce);
            _moneyList.Add(money);
        }
    }

    private void OutStack(BuyPlace target)
    {
        
            foreach (MoneyInPlayer moneyInPlayer in _moneyList)
            {
                if (!moneyInPlayer.gameObject.activeInHierarchy)
                {
                    moneyInPlayer.gameObject.SetActive(true);
                    moneyInPlayer.PushingToBuyPlace(target, this);
                    return;
                }
            }
        
    }

    private void CheckClientsQueue(CashTable cashTable)
    {
        if (cashTable.ClientCount <= 0)
        {
            return;
        }

        if (_boxing)
        {
            return;
        }

        cashTable.CreateBox(this);
        _boxing = true;
    }

    private void CheckCashMoney(CashTable cashTable)
    {
        if (cashTable.MoneyCount <= 0)
        {
            return;
        }

        if (_takeMoney)
        {
            return;
        }

        cashTable.PayMoney(this);
        _takeMoney = true;
    }

    private void CheckBank(BuyPlace buyPlace)
    {
        if (_economics.Money < buyPlace.Price)
        {
            return;
        }

        _onBuy = true;
        _economics.BuyNewPlace(buyPlace.Price);
        OutStack(buyPlace);
    }
}