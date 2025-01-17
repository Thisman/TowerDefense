using Game.Map;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public class MoveBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [Inject]
        private MapModel _mapModel;

        private List<Vector3> _path = new();
        private int currentTargetIndex = 0;

        void Start()
        {
            _path = _mapModel.EnemiesPath;
            StartMoving();
        }

        void Update()
        {
            if (_path.Count > 0 && currentTargetIndex < _path.Count)
            {
                MoveToTarget();
            }
        }

        public void StartMoving()
        {
            if (_path.Count == 0)
            {
                return;
            }

            currentTargetIndex = 0;
        }

        private void MoveToTarget()
        {
            Vector3 targetPosition = _path[currentTargetIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;

            transform.position += direction * _speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;

                currentTargetIndex++;
            }
        }
    }
}
