using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayersStack))]
    public class PlayerBuillboard: MonoBehaviour
    {
        [SerializeField] private Canvas builboard;
        private Camera _camera;

        private PlayersStack _playersStack;
        private void Start()
        {
            _camera = Camera.main;
            _playersStack = GetComponent<PlayersStack>();
            _playersStack.IsFull += ActivateBuillboard;
            _playersStack.IsFreeStack += DesactivateBuillboard;
            builboard.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _playersStack.IsFreeStack -= DesactivateBuillboard;
            _playersStack.IsFull -= ActivateBuillboard;
        }

        private void DesactivateBuillboard()
        {
            builboard.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (builboard.isActiveAndEnabled)
            {
                builboard.transform.LookAt(_camera.transform);
            }
        }

        private void ActivateBuillboard()
        {
            builboard.gameObject.SetActive(true);
        }
    }
}