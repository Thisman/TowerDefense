using Game.Map;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class Wave : MonoBehaviour
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private int _count;

        [SerializeField]
        private GameObject _enemy;

        [SerializeField]
        private float _enemySpawnDelaySec;

        public string Name => _name;

        public int Count => _count;

        public float EnemySpawnDelay => _enemySpawnDelaySec;

        public GameObject Enemy => _enemy;
    }
}
