using Game.Map;
using Game.States;
using UnityEngine;
using Zenject;

namespace Game.Scenes
{
    public class GameBootstrap : MonoBehaviour
    {
        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private GameFSM _gameStates;

        [Inject]
        private DiContainer _diContainer;

        [Inject]
        private MapModel _mapModel;

        [Inject]
        private MapPathFinder _mapPathFinder;

        [Inject]
        private CursorController _cursorController;

        public void Start()
        {
            _cursorController.SetCursor("default");

            // Game States
            _gameStates.AddState<DayState, DayStateData>(_diContainer.Instantiate<DayState>());
            _gameStates.AddState<EnemyNightState, EnemyNightStateData>(_diContainer.Instantiate<EnemyNightState>());
            _gameStates.AddState<RewardsState, RewardsStateData>(_diContainer.Instantiate<RewardsState>());

            _gameStates.SwitchState<DayState, DayStateData>(new DayStateData());

            // Player States
            _playerStates.AddState<IdleState, IdleStateData>(_diContainer.Instantiate<IdleState>());
            _playerStates.AddState<EnemyInfoState, EnemyInfoStateData>(_diContainer.Instantiate<EnemyInfoState>());
            _playerStates.AddState<SpellBookState, SpellBookStateData>(_diContainer.Instantiate<SpellBookState>());
            _playerStates.AddState<ConstructionState, ConstructionStateData>(_diContainer.Instantiate<ConstructionState>());
            _playerStates.AddState<BuildingInfoState, BuildingInfoStateData>(_diContainer.Instantiate<BuildingInfoState>());
            _playerStates.AddState<CastState, CastStateData>(_diContainer.Instantiate<CastState>());

            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());

            _mapModel.EnemiesPath = _mapPathFinder.FindEnemiesPath();

        }

        public void Update()
        {
            _gameStates.Update();
            _playerStates.Update();
        }
    }
}
