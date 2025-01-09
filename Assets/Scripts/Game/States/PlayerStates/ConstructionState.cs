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
        private CursorController _cursorController;

        private ConstructionStateData _data;

        private GameObject _tempBuilding;
        private List<Vector3Int> _highlightedArea;
        private Color _availableForBuildingColor = Color.green;
        private Color _unAvailableForBuildingColor = Color.red;

        public void Enter() { }

        public void Enter(ConstructionStateData data)
        {
            _data = data;
            _tempBuilding = CreateTempBuilding();

            _cursorController.SetCursor("building");
        }

        public void Update() {
            ResetHighlightedArea();
            HighlightArea();

            if (_tempBuilding != null)
            {
                MoveTempBuilding();
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleConfirmBuilding();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleCancelBuilding();
            }
        }

        public void Exit() {
            _cursorController.SetCursor("default");
            ResetHighlightedArea();

            if (_tempBuilding != null)
            {
                _tempBuilding = null;
            }
        }

        public ConstructionStateData GetData()
        {
            return _data;
        }

        private void HandleCancelBuilding()
        {
            GameObject.Destroy(_tempBuilding);
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleConfirmBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            _tempBuilding.gameObject.layer = 0;
            if (_mapBuilder.ConstructBuilding(position, _tempBuilding, _highlightedArea))
            {
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        private GameObject CreateTempBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            return GameObject.Instantiate(_data.Building, _mapModel.GetTileCenter(tilePosition), Quaternion.identity);
        }

        private void MoveTempBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            _tempBuilding.transform.position = _mapModel.GetTileCenter(tilePosition);
        }

        private void HighlightArea()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _highlightedArea = _mapModel.GetTilesArea(position, _mapModel.BuildingDestroySquare);
            List<Color> colors = GetTilesColor(_highlightedArea);
            _mapHighlighter.HighlightArea(_highlightedArea, colors);
        }

        private void ResetHighlightedArea()
        {
            if (_highlightedArea != null)
            {
                _mapHighlighter.ResetAreaHighlight(_highlightedArea);
            }
        }

        private List<Color> GetTilesColor(List<Vector3Int> area)
        {
            List<Color> colors = new();
            foreach (var tile in area)
            {
                var color = _mapModel.IsAvailableForBuilding(tile) ? _availableForBuildingColor : _unAvailableForBuildingColor;
                colors.Add(color);
            }

            return colors;
        }
    }
}
