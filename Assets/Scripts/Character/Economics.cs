using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Economics : MonoBehaviour
{
    [Header("InGame Menu")]
    [SerializeField] private Text _money;
    [SerializeField] private float _speedDuration;

    public int Money
    {
        get { return PlayerPrefs.GetInt("Money"); ; }
        set { PlayerPrefs.SetInt("Money", value); }
    }
   
    private void Start()
    {
        _money.text = Money.ToString();
    }

    public void GetMoney()
    {
        int result = Money + 1;
        UpdateMoney(result);
    }

    public void BuyNewPlace(int price)
    {
        int result = Money - price;
        UpdateMoney(result);
    }

    private void UpdateMoney(int result)
    {
        _money.DOCounter(Money, result, _speedDuration)
              .OnPlay(() =>
              {
                  _money.transform.DOScale(1.5f, _speedDuration)
                        .OnComplete(() => { _money.transform.DOScale(1, _speedDuration); });
              })
              .OnComplete(() => { Money = result; });
    }
}
