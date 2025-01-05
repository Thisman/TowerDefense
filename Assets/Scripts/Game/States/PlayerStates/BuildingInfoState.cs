using Game.Core;
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

        private BuildingInfoStateData _data;

        public void Enter() { }

        public void Enter(BuildingInfoStateData data)
        {
            _data = data;

            _buildingView.Show(_data.Building);
            _buildingView.OnHideView += HandleCloseView;
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleCloseView();
            }
        }

        public void Exit() {
            _buildingView.OnHideView -= HandleCloseView;
            _buildingView.Hide();
        }

        public BuildingInfoStateData GetData()
        {
            return _data;
        }

        private void HandleCloseView()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }
    }
}
