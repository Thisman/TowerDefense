using Game.Core;
using Game.Map;
using System.Collections;
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
        }

        public void Update() {
            ResetHighlightedAreaColor();
            HighlightArea();

            if (_tempBuilding != null)
            {
                MoveTempBuilding();
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleConstructBuilding();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        public void Exit() {
            if (_tempBuilding != null)
            {
                GameObject.Destroy(_tempBuilding);
                _tempBuilding = null;
            }

            ResetHighlightedAreaColor();
        }

        public ConstructionStateData GetData()
        {
            return _data;
        }

        private void HandleConstructBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            bool canBuild = _highlightedArea.FindAll(position => !_mapModel.IsAvailableForBuilding(position)).Count == 0;
            if (canBuild &&_mapBuilder.ConstructBuilding(position, _data.Building))
            {
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        private GameObject CreateTempBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.Mask.WorldToCell(position);
            return GameObject.Instantiate(_data.Building, _mapModel.GetTileCenter(tilePosition), Quaternion.identity);
        }

        private void MoveTempBuilding()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.Mask.WorldToCell(position);
            _tempBuilding.transform.position = _mapModel.GetTileCenter(tilePosition);
        }

        private void HighlightArea()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _highlightedArea = _mapModel.GetTilesArea(position, _mapModel.BuildingSquare);
            List<Color> colors = GetTilesColor(_highlightedArea);
            _mapHighlighter.HighlightArea(_highlightedArea, colors);
        }

        private void ResetHighlightedAreaColor()
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
