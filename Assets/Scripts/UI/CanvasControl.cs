﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CanvasControl : MonoBehaviour
    {

        [Header("Panels")]
        [SerializeField] private CanvasGroup _panelMenu;
        [SerializeField] private CanvasGroup _panelInGame;
        [SerializeField] private CanvasGroup _panelWin;
        [SerializeField] private CanvasGroup _panelLost;
        [SerializeField] private CanvasGroup _panelHeader;

        private List<CanvasGroup> _canvasGroupes = new List<CanvasGroup>();
        private LevelEventService _levelManager;
        [Inject]
        void Construct(LevelEventService LevelManager)
        {
            _levelManager = LevelManager;
        }

        private void Start()
        {
            _levelManager.OnLevelStart += OnLevelStart;
            _levelManager.OnLateWin += OnLevelWin;
            _levelManager.OnLateLost += OnLevelLost;

            _canvasGroupes.Add(_panelMenu);
            _canvasGroupes.Add(_panelInGame);
            _canvasGroupes.Add(_panelWin);
            _canvasGroupes.Add(_panelLost);

            _panelHeader.alpha = 1;
            _panelHeader.interactable = true;
            _panelHeader.blocksRaycasts = true;
            SwitchOnAllCanvasObjects();
            ActivateUIScreen(_panelMenu);
        }

        private void OnDisable()
        {
            _levelManager.OnLevelStart -= OnLevelStart;
            _levelManager.OnLateWin -= OnLevelWin;
            _levelManager.OnLateLost -= OnLevelLost;
        }

        public void LevelStart()
        {
            _levelManager.LevelStart();
        }

        public void LoadNextLevel()
        {
            _levelManager.LoadNextLevel();
        }

        private void SwitchOnAllCanvasObjects()
        {
            foreach (CanvasGroup cG in _canvasGroupes)
            {
                cG.gameObject.SetActive(true);
            }
        }

        private void OnLevelStart()
        {
            _levelManager.LevelPlay();

            ActivateUIScreen(_panelInGame);
        }

        private void OnLevelWin()
        {
            Debug.Log("Level Win");
            ActivateUIScreen(_panelWin);
        }

        private void OnLevelLost()
        {
            Debug.Log("Level Lost");
            ActivateUIScreen(_panelLost);
        }

       private void ActivateUIScreen(CanvasGroup uiScreen)
        {
            foreach (CanvasGroup cGr in _canvasGroupes)
            {
                if (cGr != uiScreen)
                {
                    cGr.alpha = 0;
                    cGr.interactable = false;
                    cGr.blocksRaycasts = false;
                }
                else
                {
                    cGr.alpha = 1;
                    cGr.interactable = true;
                    cGr.blocksRaycasts = true;
                }
            }
        }
    }
}
