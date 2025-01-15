using Game.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public class Spawner : MonoBehaviour
    {
        public Action OnWaveSpawnEnded;

        [SerializeField]
        private float _waveSpawnDelaySec;

        [SerializeField]
        private List<WaveGenerator.Wave> _waves = new();

        [Inject]
        private DiContainer _diContainer;

        [Inject]
        private MapModel _mapModel;

        [Inject]
        private WaveGenerator _waveGenerator;

        private List<EnemyModel> _enemies = new();
        private int _currentWaveIndex;

        public void Start()
        {
            _waves = _waveGenerator.ProcessWaves();
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        public void OnDestroy()
        {
            StopAllCoroutines();
            _enemies.ForEach(enemyModel => GameObject.Destroy(enemyModel.gameObject));
        }

        public void StartSpawnEnemies()
        {
            StartCoroutine(StartSpawn());
        }

        public void StopSpawnEnemies()
        {
            StopAllCoroutines();
        }
        
        private IEnumerator StartSpawn()
        {
            if (_currentWaveIndex > _waves.Count)
            {
                _currentWaveIndex = 0;
            }

            var wave = _waves[_currentWaveIndex];
            foreach (var batch in wave.Batches)
            {
                yield return new WaitForSeconds(_waveSpawnDelaySec);
                yield return StartCoroutine(SpawnWave(batch));
            }

            yield return new WaitUntil(() => _enemies.Count == 0);
            OnWaveSpawnEnded?.Invoke();
        }

        private IEnumerator SpawnWave(WaveGenerator.Batch batch)
        {
            GameObject enemiesSpawn = _mapModel.EnemiesSpawn;
            for (int i = 0; i < batch.Count; i++)
            {
                GameObject enemy = _diContainer.InstantiatePrefab(batch.Enemy, enemiesSpawn.transform.position, Quaternion.identity, enemiesSpawn.transform);
                EnemyModel enemyModel = enemy.GetComponent<EnemyModel>();
                AddEnemyToPool(enemyModel);
                enemyModel.OnEnemyDestroy += RemoveEnemyFromPool;

                yield return new WaitForSeconds(4);
            }
        }

        private void AddEnemyToPool(EnemyModel enemyModel)
        {
            _enemies.Add(enemyModel);
        }

        private void RemoveEnemyFromPool(EnemyModel enemyModel)
        {
            enemyModel.OnEnemyDestroy += RemoveEnemyFromPool;
            _enemies.Remove(enemyModel);
        }
    }
}
