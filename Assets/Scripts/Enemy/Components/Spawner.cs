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
        private Wave[] _waves;

        [Inject]
        private DiContainer _diContainer;

        [Inject]
        private MapModel _mapModel;

        private List<EnemyModel> _enemies = new();

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
            foreach (var wave in _waves)
            {
                yield return new WaitForSeconds(_waveSpawnDelaySec);
                yield return StartCoroutine(SpawnWave(wave));
            }

            yield return new WaitUntil(() => _enemies.Count == 0);
            OnWaveSpawnEnded?.Invoke();
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            GameObject enemiesSpawn = _mapModel.EnemiesSpawn;
            for (int i = 0; i < wave.Count; i++)
            {
                GameObject enemy = _diContainer.InstantiatePrefab(wave.Enemy, enemiesSpawn.transform.position, Quaternion.identity, enemiesSpawn.transform);
                EnemyModel enemyModel = enemy.GetComponent<EnemyModel>();
                AddEnemyToPool(enemyModel);
                enemyModel.OnEnemyDestroyed += RemoveEnemyFromPool;

                yield return new WaitForSeconds(4);
            }
        }

        private void AddEnemyToPool(EnemyModel enemyModel)
        {
            _enemies.Add(enemyModel);
        }

        private void RemoveEnemyFromPool(EnemyModel enemyModel)
        {
            enemyModel.OnEnemyDestroyed += RemoveEnemyFromPool;
            _enemies.Remove(enemyModel);
        }
    }
}
