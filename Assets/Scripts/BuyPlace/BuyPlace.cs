using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.BuyPlace
{
    public class BuyPlace: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private int _price;
        [SerializeField] private GameObject _prefab;
        public int Price => _price;

        private Camera _camera;
        private BoxCollider _collider;
        
        private void Start()
        {
            _camera=Camera.main;
            _collider = GetComponent<BoxCollider>();
            _priceText.text = _price.ToString();
        }

        private void Update()
        {
            _canvas.transform.LookAt(_camera.transform);
        }


        public void StackIn()
        {
            GetNewPlace();
        }

        private void GetNewPlace()
        {
            var go = Instantiate(_prefab, gameObject.transform.position, gameObject.transform.rotation);
            StartCoroutine(DestroyPlace());
        }

        private IEnumerator DestroyPlace()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}