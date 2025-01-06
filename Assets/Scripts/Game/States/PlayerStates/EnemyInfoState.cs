using Game.Core;
using UnityEngine;
using Zenject;
using Game.UI;

namespace Game.States
{
    public struct EnemyInfoStateData {
        readonly public GameObject Enemy;

        public EnemyInfoStateData(GameObject enemy)
        {
            Enemy = enemy;
        }
    }

    public class EnemyInfoState : IState<EnemyInfoStateData>
    {
        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private EnemyView _enemyView;

        private EnemyInfoStateData _data;

        public void Enter() { }

        public void Enter(EnemyInfoStateData data)
        {
            _data = data;

            _enemyView.OnViewHidden += HandleViewHidden;
            _enemyView.Show(_data.Enemy);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleViewHidden();
            }
        }

        public void Exit()
        {
            _enemyView.OnViewHidden -= HandleViewHidden;
            _enemyView.Hide();
        }

        public EnemyInfoStateData GetData()
        {
            return _data;
        }

        private void HandleViewHidden()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }
    }
}
