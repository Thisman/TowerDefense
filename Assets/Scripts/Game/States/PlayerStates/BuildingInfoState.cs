using Game.Building;
using Game.Buildings;
using Game.Core;
using Game.Map;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public struct BuildingInfoStateData {
        readonly public GameObject Building;

        public BuildingInfoStateData(GameObject building)
        {
            Building = building;
        }
    }

    public class BuildingInfoState : IState<BuildingInfoStateData>
    {
        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private BuildingView _buildingView;

        [Inject]
        private MapBuilder _mapBuilder;

        private BuildingHighlighter _buildingHighlighter;

        private BuildingInfoStateData _data;

        public void Enter() { }

        public void Enter(BuildingInfoStateData data)
        {
            _data = data;

            _buildingView.Show(_data.Building);
            _buildingView.OnViewHidden += HandleViewHidden;
            _buildingView.OnBuildingDeleted += HandleBuildDeleted;

            _buildingHighlighter = _data.Building.GetComponent<BuildingHighlighter>();
            _buildingHighlighter.ShowEffectArea();
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleViewHidden();
            }
        }

        public void Exit() {
            _buildingView.OnBuildingDeleted -= HandleBuildDeleted;
            _buildingView.OnViewHidden -= HandleViewHidden;
            _buildingView.Hide();

            _buildingHighlighter.HideEffectArea();
        }

        public BuildingInfoStateData GetData()
        {
            return _data;
        }

        private void HandleViewHidden()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleBuildDeleted()
        {
            _mapBuilder.RemoveBuilding(_data.Building);
            HandleViewHidden();
        }
    }
}
