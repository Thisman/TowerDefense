using Game.Core;
using Game.Map;
using Game.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject]
    private PathFinder pathFinder;

    [Inject]
    private Highlighter highlighter;

    [Inject]
    private FSM _gameStates;

    [Inject]
    private DiContainer _diContainer;

    public void Start()
    {
        _gameStates.AddState<IdleState, IdleStateData>(_diContainer.Instantiate<IdleState>());
        _gameStates.AddState<ShopState, ShopStateData>(_diContainer.Instantiate<ShopState>());
        _gameStates.AddState<BuildingState, BuildingStateData>(_diContainer.Instantiate<BuildingState>());
        _gameStates.AddState<BuildingInfoState, BuildingInfoStateData>(_diContainer.Instantiate<BuildingInfoState>());

        _gameStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());

        List<Vector3> path = pathFinder.GenerateEnemiesPath();
        highlighter.HighlightPath(path, Color.blue);
    }

    public void Update()
    {
        _gameStates.Update();
    }
}
