using Game.Core;
using Game.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject]
    private PlayerFSM _playerStates;

    [Inject]
    private GameFSM _gameStates;

    [Inject]
    private DiContainer _diContainer;

    public void Start()
    {
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

    }

    public void Update()
    {
        _gameStates.Update();
        _playerStates.Update();
    }
}
