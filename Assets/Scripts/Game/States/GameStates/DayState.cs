using Game.Core;
using UnityEngine;
using Zenject;
using Game.UI;

namespace Game.States
{
    public struct DayStateData {}

    public class DayState : IState<DayStateData>
    {
        [Inject]
        private MenuView _menuView;

        [Inject]
        private GameFSM _gameStates;

        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private MusicController _musicController;

        private DayStateData _data;

        public void Enter() { }

        public void Enter(DayStateData data) {
            _data = data;

            StartMusic();

            _menuView.SwitchGoToNextButtonState(true);
            _menuView.ChooseBuilding += HandleChooseBuilding;
            _menuView.GoToNextState += HandleGoToNight;
        }

        public void Update() { }

        public void Exit() {
            _menuView.SwitchGoToNextButtonState(false);
            _menuView.ChooseBuilding -= HandleChooseBuilding;
            _menuView.GoToNextState -= HandleGoToNight;
        }

        public DayStateData GetData()
        {
            return _data;
        }

        private void HandleChooseBuilding(GameObject building)
        {
            _playerStates.SwitchState<ConstructionState, ConstructionStateData>(new ConstructionStateData(building));
        }

        private void HandleGoToNight()
        {
            _gameStates.SwitchState<EnemyNightState, EnemyNightStateData>(new EnemyNightStateData());
        }

        private void StartMusic()
        {
            _musicController.FadeVolume(0, 1, () =>
            {
                _musicController.PlayMusic("Morning");
                _musicController.FadeVolume(1, 1);
            });
        }
    }
}
