using Game.Core;
using Game.Map;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
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

        private BuildingInfoStateData _data;

        public void Enter() { }

        public void Enter(BuildingInfoStateData data)
        {
            _data = data;

            _buildingView.Show(_data.Building);
            _buildingView.OnHideView += HandleHideView;
            _buildingView.OnDeleteBuilding += HandleDeleteBuilding;
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleHideView();
            }
        }

        public void Exit() {
            _buildingView.OnDeleteBuilding -= HandleDeleteBuilding;
            _buildingView.OnHideView -= HandleHideView;
            _buildingView.Hide();
        }

        public BuildingInfoStateData GetData()
        {
            return _data;
        }

        private void HandleHideView()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleDeleteBuilding()
        {
            _mapBuilder.RemoveBuilding(_data.Building);
            HandleHideView();
        }
    }
}
