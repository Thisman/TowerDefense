using Game.Towers;
using Game.Castle;
using Game.Core;
using Game.Map;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public struct TowerInfoStateData {
        readonly public GameObject Tower;

        public TowerInfoStateData(GameObject tower)
        {
            Tower = tower;
        }
    }

    public class TowerInfoState : IState<TowerInfoStateData>
    {
        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private TowerView _towerView;

        [Inject]
        private MapBuilder _mapBuilder;

        [Inject]
        private ResourcesModel _resourcesModel;

        private TowerHighlighter _towerHighlighter;

        private TowerInfoStateData _data;

        public void Enter() { }

        public void Enter(TowerInfoStateData data)
        {
            _data = data;

            _towerView.Show(_data.Tower);
            _towerView.OnViewHidden += HandleViewHidden;
            _towerView.OnTowerDeleting += HandleBuildDeleted;

            _towerHighlighter = _data.Tower.GetComponent<TowerHighlighter>();
            _towerHighlighter.ShowEffectArea();
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleViewHidden();
            }
        }

        public void Exit() {
            _towerView.OnTowerDeleting -= HandleBuildDeleted;
            _towerView.OnViewHidden -= HandleViewHidden;
            _towerView.Hide();

            _towerHighlighter.HideEffectArea();
        }

        public TowerInfoStateData GetData()
        {
            return _data;
        }

        private void HandleViewHidden()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleBuildDeleted()
        {
            TowerModel towerModel = _data.Tower.GetComponent<TowerModel>();
            _resourcesModel.ChangeMoney(towerModel.Price);
            _mapBuilder.RemoveTower(_data.Tower);
            HandleViewHidden();
        }
    }
}
