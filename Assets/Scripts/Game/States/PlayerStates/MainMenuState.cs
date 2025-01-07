using Game.Core;
using Game.Map;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.States
{
    public struct MainMenuStateData { }

    public class MainMenuState : IState<MainMenuStateData>
    {
        [Inject]
        private MainMenuView _mainMenuView;

        [Inject]
        private MusicController _musicController;

        private MainMenuStateData _data;

        public void Enter() { }

        public void Enter(MainMenuStateData data)
        {
            _data = data;

            _mainMenuView.OnGameStarted += HandleGameStarted;
            _mainMenuView.OnGameQuit += HandleQuitGame;

            _musicController.PlayMusic("TowerDefense_BackgroundSound");
        }

        public void Update() { }

        public void Exit() {
            _mainMenuView.OnGameStarted -= HandleGameStarted;
            _mainMenuView.OnGameQuit -= HandleQuitGame;

            _musicController.StopMusic();
        }

        public MainMenuStateData GetData()
        {
            return _data;
        }

        private void HandleGameStarted()
        {
            SceneManager.LoadScene("Demo_v2");
        }

        private void HandleQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
