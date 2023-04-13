using System.Collections;
using DefaultNamespace.Clients;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

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

        private ClientsGenerator _clientsGenerator;

        [Inject]
        private void Construct(ClientsGenerator clientsGenerator)
        {
            _clientsGenerator = clientsGenerator;
        }
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
            if (go.CompareTag("Store"))
            {
                _clientsGenerator.OpenNewStore(go.transform);
            }
            StartCoroutine(DestroyPlace());
        }

        private IEnumerator DestroyPlace()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }
}