using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesSpawnController: MonoBehaviour
{
    [Inject]
    private MapModel _mapModel;

    [Inject]
    private EnemiesSpawnModel _enemiesSpawnModel;

    [Inject]
    private DiContainer _diContainer;

    private bool _isSpawnRunning;

    public IEnumerator StartSpawnWaves()
    {
        if (_mapModel == null || _enemiesSpawnModel == null)
        {
            yield return null;
        }

        _isSpawnRunning = true;
        foreach (EnemiesWaveModel wave in _enemiesSpawnModel.Waves)
        {
            _enemiesSpawnModel.CurrentWave = wave;
            yield return StartCoroutine(SpawnWave(wave));
        }

        _enemiesSpawnModel.CurrentWave = null;
    }

    public void StopSpawnWaves()
    {
        this.StopAllCoroutines();
        _isSpawnRunning = false;
    }

    public bool IsSpawnRunning()
    {
        return _isSpawnRunning;
    }

    private IEnumerator SpawnWave(EnemiesWaveModel wave)
    {
        for (int i = 0; i < wave.Limit; i++)
        {
            _diContainer.InstantiatePrefab(wave.EnemyPrefab, _mapModel.EnemiesSpawnPosition.transform.position, Quaternion.identity, gameObject.transform.parent);
            float spawnRandomDelay = Random.Range(-_enemiesSpawnModel.SpawnDelayOffsetSec, _enemiesSpawnModel.SpawnDelayOffsetSec);
            yield return new WaitForSeconds(wave.SpawnDelay + spawnRandomDelay);
        }

        yield return new WaitForSeconds(_enemiesSpawnModel.WavesDelaySec);
    }
}
