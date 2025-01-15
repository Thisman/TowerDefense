using Game.Core;
using UnityEngine;
using Zenject;
using Game.Environment;
using Game.Towers;
using Game.Castle;

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

        [Inject]
        private SkyBoxController _skyBoxController;

        [Inject]
        private ResourcesModel _resourcesModel;

        private DayStateData _data;

        public void Enter() { }

        public void Enter(DayStateData data) {
            _data = data;

            StartMusic();

            _skyBoxController.SetDayLightning();

            _menuView.SwitchGoToNextButtonState(true);
            _menuView.OnTowerChosen += HandleTowerChosen;
            _menuView.OnStateChanging += HandleDayEnded;
        }

        public void Update() { }

        public void Exit() {
            _menuView.SwitchGoToNextButtonState(false);
            _menuView.OnTowerChosen -= HandleTowerChosen;
            _menuView.OnStateChanging -= HandleDayEnded;
        }

        public DayStateData GetData()
        {
            return _data;
        }

        private void HandleTowerChosen(GameObject tower)
        {
            TowerModel towerModel = tower.GetComponent<TowerModel>();
            if (_resourcesModel.ChangeMoney(-towerModel.Price))
            {
                _playerStates.SwitchState<ConstructionState, ConstructionStateData>(new ConstructionStateData(tower));
            }
        }

        private void HandleDayEnded()
        {
            _gameStates.SwitchState<EnemyNightState, EnemyNightStateData>(new EnemyNightStateData());
        }

        private void StartMusic()
        {
            _musicController.FadeVolume(0, 1, () =>
            {
                _musicController.PlayMusic("Morning");
                _musicController.FadeVolume(.3f, 1);
            });
        }
    }
}
