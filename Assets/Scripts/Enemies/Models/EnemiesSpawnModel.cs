using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesSpawnModel
{
    public Action<EnemiesWaveModel> OnCurrentWaveChanges;

    private float _wavesDelaySec = 1.5f;
    private float _spawnDelayOffsetSec = .5f;

    private EnemiesWaveModel _currentWave;
    private List<EnemiesWaveModel> _waves = new();

    public EnemiesSpawnModel()
    {
        GameObject[] wavePrefabs = Resources.LoadAll<GameObject>("Prefabs/Waves");
        foreach(var prefab in wavePrefabs)
        {
            _waves.Add(prefab.GetComponent<EnemiesWaveModel>());
        }
    }

    public float WavesDelaySec { get { return _wavesDelaySec; } }

    public float SpawnDelayOffsetSec { get { return _spawnDelayOffsetSec; } }

    public List<EnemiesWaveModel> Waves { get { return _waves; } }

    public EnemiesWaveModel CurrentWave {
        get { return _currentWave; }
        set {
            _currentWave = value;
            OnCurrentWaveChanges.Invoke(_currentWave);
        }
    }
}
