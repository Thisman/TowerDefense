using Game.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            _enemyView.OnViewHidden += HandleHideView;
            _enemyView.Show(_data.Enemy);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleHideView();
            }
        }

        public void Exit()
        {
            _enemyView.OnViewHidden -= HandleHideView;
            _enemyView.Hide();
        }

        public EnemyInfoStateData GetData()
        {
            return _data;
        }

        private void HandleHideView()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }
    }
}
