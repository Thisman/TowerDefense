using Game.Building;
using Game.Buildings;
using Game.Castle;
using Game.Core;
using Game.Map;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public struct ConstructionStateData {
        readonly public GameObject Building;

        public ConstructionStateData(GameObject building)
        {
            Building = building;
        }
    }

    public class ConstructionState : IState<ConstructionStateData>
    {
        [Inject]
        private MapModel _mapModel;

        [Inject]
        private MapBuilder _mapBuilder;

        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private MapHighlighter _mapHighlighter;

        [Inject]
        private MapTerraformer _mapTerraformer;

        [Inject]
        private CursorController _cursorController;

        [Inject]
        private ResourcesModel _resourcesModel;

        [Inject]
        private DiContainer _diContainer;

        private BuildingHighlighter _buildingHighlighter;

        private ConstructionStateData _data;

        private GameObject _tempBuilding;

        public void Enter() { }

        public void Enter(ConstructionStateData data)
        {
            _data = data;
            _cursorController.SetCursor("building");
            _tempBuilding = HandleStartConstruction();

            _buildingHighlighter = _tempBuilding.GetComponent<BuildingHighlighter>();
            _buildingHighlighter.ShowEffectArea();
        }

        public void Update()
        {
            if (_tempBuilding != null)
            {
                _mapTerraformer.ShowPropsInArea(_tempBuilding.transform.position);
                _mapHighlighter.ResetAreaHighlightForBuilding(_tempBuilding.transform.position);
                MoveTempBuilding();
                _mapTerraformer.HidePropsInArea(_tempBuilding.transform.position);
                _mapHighlighter.HighlightAreaForBuilding(_tempBuilding.transform.position);
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleConfirmConstruction();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleCancelConstruction();
            }
        }

        public void Exit() {
            _cursorController.SetCursor("default");
            _mapTerraformer.ShowPropsInArea(_tempBuilding.transform.position);
            _mapHighlighter.ResetAreaHighlightForBuilding(_tempBuilding.transform.position);

            _buildingHighlighter.HideEffectArea();

            if (_tempBuilding != null)
            {
                _tempBuilding = null;
            }
        }

        public ConstructionStateData GetData()
        {
            return _data;
        }

        private void HandleCancelConstruction()
        {
            BuildingModel buildingModel = _tempBuilding.GetComponent<BuildingModel>();
            _resourcesModel.ChangeMoney(buildingModel.Price);
            GameObject.Destroy(_tempBuilding);
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleConfirmConstruction()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            if (_mapBuilder.ConstructBuilding(position, _tempBuilding))
            {
                _tempBuilding.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        private GameObject HandleStartConstruction()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            return _diContainer.InstantiatePrefab(_data.Building, _mapModel.GetTileCenter(tilePosition), Quaternion.identity, _mapModel.Castle.transform);
        }

        private void MoveTempBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            _tempBuilding.transform.position = _mapModel.GetTileCenter(tilePosition);
        }
    }
}
