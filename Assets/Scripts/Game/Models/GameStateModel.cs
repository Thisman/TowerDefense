using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum GameState
{
    IDLE,
    SELECT_TOWER,
    PLACE_TOWER,
}

public class GameStateModel
{
    public ReactiveProperty<GameState> State { get; private set; } = new(GameState.IDLE);
}
