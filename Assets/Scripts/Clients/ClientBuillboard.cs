using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Clients
{
    [RequireComponent(typeof(ClientStack))]
    public class ClientBuillboard: MonoBehaviour
    {
        [SerializeField]
        private Canvas builboard;
        [SerializeField]
        public Image _imageWishes;
        [SerializeField]
        private Image _imageCash;
        [SerializeField]
        private TextMeshProUGUI _vegetableCountText;
        private Camera _camera;

        private ClientStack _clientStack;
        private int _vegetableCount;

        private void Start()
        {
            _camera = Camera.main;
            _clientStack = GetComponent<ClientStack>();
            _clientStack.IsFull += SwitchBuillboard;
            _clientStack.GetVegetable += UpdateCountVegetable;
            _clientStack.IsEmpty += DesActivateBillboard;
            OnStart();
        }

        private void Update()
        {
            if (builboard.isActiveAndEnabled)
            {
                builboard.transform.LookAt(_camera.transform);
            }
        }

        private void OnDestroy()
        {
            _clientStack.IsFull -= SwitchBuillboard;
            _clientStack.GetVegetable -= UpdateCountVegetable;
            _clientStack.IsEmpty -= DesActivateBillboard;
        }

        private void OnStart()
        {
            SetVegetableCountText();
            _imageCash.gameObject.SetActive(false);
        }

        private void UpdateCountVegetable()
        {
            _vegetableCount--;
            SetVegetableCountText();
        }

        private void SetVegetableCountText()
        {
            _vegetableCount = _clientStack.WisheVegetables;
            _vegetableCountText.text = "x" +_vegetableCount.ToString();
        }

        private void SwitchBuillboard()
        {
            _imageCash.gameObject.SetActive(true);
            _imageWishes.gameObject.SetActive(false);
        }

        private void DesActivateBillboard()
        {
            builboard.gameObject.SetActive(false);
        }
    }
}