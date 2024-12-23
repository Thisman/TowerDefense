using Game.Building;
using Game.Buildings;
using Game.Core;
using Game.Map;
using Game.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.States
{
    public struct BuildingInfoStateData
    {
        public GameObject Tower;
        
        public BuildingInfoStateData(GameObject tower)
        {
            Tower = tower;
        }
    }

    public class BuildingInfoState : IState<BuildingInfoStateData>
    {
        [Inject]
        private FSM _gameStates;

        [Inject]
        private BuildingRenderer _buildingRenderer;

        [Inject]
        private MapModel _mapModel;

        [Inject]
        private Game.Map.Builder _builder;

        [Inject]
        private Terraformer _terraformer;

        private BuildingInfoStateData _data;

        public void Enter() { }

        public void Enter(BuildingInfoStateData data)
        {
            _data = data;

            _buildingRenderer.OnDeleteBuildingButtonClicked += HandleDeleteBuilding;
            BuildingTemplate template = _data.Tower.GetComponent<BuildingModel>()?.Template;

            if (template != null)
            {
                _buildingRenderer.Render(template);
                _buildingRenderer.Show();
            }
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        public void Exit()
        {
            _buildingRenderer.OnDeleteBuildingButtonClicked -= HandleDeleteBuilding;
            _buildingRenderer.Hide();
        }

        public BuildingInfoStateData GetData()
        {
            return _data;
        }

        private void HandleDeleteBuilding()
        {
            BuildingModel towerModel = _data.Tower.gameObject.GetComponent<BuildingModel>();
            if (towerModel != null && _builder.TryRemove(_data.Tower.gameObject))
            {
                List<Vector3Int> area = _mapModel.GetTilesArea(towerModel.Position, towerModel.Template.Square);
                List<TileBase> tiles = area.Select(position => _mapModel.GetTilesByTemplateName("ConstructionTile").Random()).ToList();

                _terraformer.ChangeTileArea(area, tiles);
                _gameStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }
    }
}
