using Game.Enemies;
using Game.Bullets;
using ModestTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Towers
{
    [RequireComponent(typeof(TowerStatsModel))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class AttackBehavior : MonoBehaviour
    {
        [SerializeField]
        private TowerStatsModel _towerStatsModel;

        [SerializeField]
        private BulletModel _tempBulletPrefab;

        private List<EnemyModel> _targets = new();
        private float _shootTimer = 0;

        public void Update()
        {
            _shootTimer -= Time.deltaTime;

            if (_shootTimer <= 0f)
            {
                ShootAtTargetObject();
                _shootTimer = _towerStatsModel.ReloadTimeSec;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyModel enemyModel = other.gameObject.GetComponent<EnemyModel>();
                enemyModel.OnEnemyDestroy += HandleEnemyDestroyed;
                _targets.Add(enemyModel);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            EnemyModel enemyModel = other.gameObject.GetComponent<EnemyModel>();
            HandleEnemyDestroyed(enemyModel);
        }

        private void HandleEnemyDestroyed(EnemyModel enemyModel)
        {
            if (_targets.Contains(enemyModel))
            {
                enemyModel.OnEnemyDestroy -= HandleEnemyDestroyed;
                _targets.Remove(enemyModel);
            }
        }

        private void ShootAtTargetObject()
        {
            if (!_targets.IsEmpty() && _targets[0] != null)
            {
                BulletModel bulletModel = Instantiate(_tempBulletPrefab, gameObject.transform.position, Quaternion.identity);
                bulletModel.TargetEnemy = _targets[0];
            }
        }
    }
}
