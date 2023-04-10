using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayersStack))]
    public class PlayerBuilboard: MonoBehaviour
    {
        [SerializeField]
        private Canvas builboard;
        private Camera _camera;

        private PlayersStack _playersStack;
        private void Start()
        {
            _camera = Camera.main;
            _playersStack = GetComponent<PlayersStack>();
            _playersStack.IsFull += ActivateBuilboard;
            builboard.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (builboard.isActiveAndEnabled)
            {
                builboard.transform.LookAt(_camera.transform);
            }
        }

        private void ActivateBuilboard()
        {
            builboard.gameObject.SetActive(true);
        }
    }
}