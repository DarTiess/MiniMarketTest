using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMoneyStack : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [Header("Magnet")]
    [SerializeField] private float _speed;
    [SerializeField] private float timeToScale;

    private List<GameObject> _coinsList = new List<GameObject>();
    private Vector3 sizeScale;
    private Economics _economics;

    [Inject]
    private void Construct(Economics economics)
    {
        _economics = economics;
    }
    private void Start()
    {
        sizeScale = _coinPrefab.transform.localScale;
    }
    public void CreateCoinsPull(int coinsLimit)
    {
        for (int i = 0; i < coinsLimit; i++)
        {
            GameObject coin = Instantiate(_coinPrefab, transform.position, transform.rotation);
            coin.gameObject.SetActive(false);
            _coinsList.Add(coin);
        }
    }

    public void PushCoinsToBank(Transform originPosition)
    {
        foreach (GameObject coin in _coinsList)
        {
            if (!coin.activeInHierarchy)
            {
                coin.transform.position = originPosition.position;
                coin.transform.localScale = sizeScale;
                coin.SetActive(true);

                StartCoroutine(MagnetMoneyToCanvas(coin));
                break;
            }
        }
    }

    private IEnumerator MagnetMoneyToCanvas(GameObject obj)
    {
        Transform dest = _economics.MoneyText.gameObject.transform;
        if (dest == null) yield break;

        obj.transform.DOScale(0, timeToScale);
        _economics.BuyCoins(1);
        while (Vector3.Distance(obj.transform.position, dest.position) > 0.1f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, dest.position, _speed);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        obj.gameObject.SetActive(false);
    }
}
