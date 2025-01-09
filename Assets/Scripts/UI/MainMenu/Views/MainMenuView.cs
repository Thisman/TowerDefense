using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public Action OnGameStarted;

        public Action OnGameQuit;

        [SerializeField]
        private Button _startGameButtonUI;

        [SerializeField]
        private Button _quitGameButtonUI;

        public void Start()
        {
            _startGameButtonUI.onClick.AddListener(HandleStartGameButtonClicked);
            _quitGameButtonUI.onClick.AddListener(HandleQuitGameButtonClicked);
        }

        public void OnDestroy()
        {
            _startGameButtonUI.onClick.RemoveAllListeners();
            _quitGameButtonUI.onClick.RemoveAllListeners();
        }

        private void HandleStartGameButtonClicked()
        {
            OnGameStarted?.Invoke();
        }

        private void HandleQuitGameButtonClicked()
        {
            OnGameQuit?.Invoke();
        }
    }
}
