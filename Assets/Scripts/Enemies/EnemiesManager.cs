using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesManager : MonoBehaviour
{

    [SerializeField]
    private EnemiesSpawnRenderer _enemiesSpawnRenderer;

    [SerializeField]
    private EnemiesSpawnController _enemiesSpawnController;

    [Inject]
    private GameTimeModel _gameTimeModel;

    public void Start() { }

    public void Update()
    {
        GameTimeData gameTimeAndDay = _gameTimeModel.GetTimeData();

        if (_gameTimeModel.IsDay() && _enemiesSpawnController.IsSpawnRunning())
        {
            _enemiesSpawnController.StopSpawnWaves();
        }

        if (_gameTimeModel.IsNight() && !_enemiesSpawnController.IsSpawnRunning())
        {
            StartCoroutine(_enemiesSpawnController.StartSpawnWaves());
        }
    }
}
