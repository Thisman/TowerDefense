using Game.Map;
using Game.States;
using UnityEngine;
using Zenject;

namespace Game.Scenes
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private DiContainer _diContainer;

        public void Start()
        {
            // Player States
            _playerStates.AddState<MainMenuState, MainMenuStateData>(_diContainer.Instantiate<MainMenuState>());

            _playerStates.SwitchState<MainMenuState, MainMenuStateData>(new MainMenuStateData());
        }

        public void Update()
        {
            _playerStates.Update();
        }
    }
}
