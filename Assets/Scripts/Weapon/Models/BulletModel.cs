using Game.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
    public class BulletModel : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _damage;

        private EnemyModel _targetEnemy;

        public float Speed => _speed;

        public float Damage => _damage;

        public EnemyModel TargetEnemy
        {
            get { return _targetEnemy; }
            set { _targetEnemy = value; }
        }
    }
}
