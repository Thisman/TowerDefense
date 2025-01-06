using Game.Enemies;
using Game.Weapons;
using ModestTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Buildings
{
    [RequireComponent(typeof(BuildingStatsModel))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class AttackBehavior : MonoBehaviour
    {
        [SerializeField]
        private BuildingStatsModel _buildingStatsModel;

        [SerializeField]
        private BulletModel _tempBulletPrefab;

        private List<EnemyModel> _targets = new();
        private float _shootTimer = 0;

        public void Update()
        {
            _shootTimer -= Time.deltaTime;

            if (_shootTimer <= 0f)
            {
                if (!_targets.IsEmpty() && _targets[0] != null)
                {
                    ShootAtTargetObject(_targets[0]);
                }
                _shootTimer = _buildingStatsModel.ReloadTimeSec;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyModel enemyModel = other.gameObject.GetComponent<EnemyModel>();
                enemyModel.OnEnemyDestroyed += HandleEnemyDestroyed;
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
                enemyModel.OnEnemyDestroyed -= HandleEnemyDestroyed;
                _targets.Remove(enemyModel);
            }
        }

        private void ShootAtTargetObject(EnemyModel target)
        {
            BulletModel bulletModel = Instantiate(_tempBulletPrefab, gameObject.transform.position, Quaternion.identity);
            bulletModel.TargetEnemy = target;
        }
    }
}
