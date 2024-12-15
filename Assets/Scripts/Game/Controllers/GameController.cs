using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float _timeSpeedQ = 1f;

    [Inject]
    private GameTimeModel _gameTimeModel;

    [Inject]
    private GameStateModel _gameStateModel;

    public void Start()
    {
        _gameTimeModel.Timestamp = _gameTimeModel.StartTimeStamp;
    }

    public void FixedUpdate()
    {
        _gameTimeModel.RealTimestamp += Time.deltaTime;
        _gameTimeModel.Timestamp += _timeSpeedQ * Time.deltaTime;
    }

    public void SetGameTime(float time)
    {
        _gameTimeModel.Timestamp = time;
    }

    public void ChangeGameState(GameState state)
    {
        _gameStateModel.State.Value = state;
    }

    public void SkipDay()
    {
        float SecondsInDay = 86400;

        float secondsPassedToday = _gameTimeModel.Timestamp % SecondsInDay;

        // (TODO) make better later
        float targetSecondsOfDay = 19 * 3600;

        float secondsUntilTarget = targetSecondsOfDay - secondsPassedToday;

        if (secondsUntilTarget < 0)
        {
            secondsUntilTarget += SecondsInDay;
        }

        SetGameTime(_gameTimeModel.Timestamp + secondsUntilTarget);
    }
}
