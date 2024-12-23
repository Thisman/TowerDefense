using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using UnityEngine.Tilemaps;
using Game.Map;
using Zenject;
using UnityEngine.U2D;
using Zenject.Asteroids;
using Game.Buildings;
using System.Linq;
using ModestTree;
using Game.Building;

namespace Game.States
{
    public struct BuildingStateData
    {
        public BuildingTemplate Building;

        public BuildingStateData(BuildingTemplate building)
        {
            Building = building;
        }
    }

    public class BuildingState : IState<BuildingStateData>
    {
        [Inject]
        private FSM _gameStates;

        [Inject]
        private MapModel _mapModel;

        [Inject]
        private Highlighter _highlighter;

        [Inject]
        private Game.Map.Builder _builder;

        [Inject]
        private Game.Map.Terraformer _terraformer;

        private BuildingStateData _data;
        private List<Vector3Int> _lastHighlightedArea;

        public void Enter() { }

        public void Enter(BuildingStateData data)
        {
            _data = data;
        }

        public void Update() {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            ResetHighlighterArea();
            HighlightArea(mouseWorldPos);

            if (Input.GetMouseButtonDown(0))
            {
                Build(mouseWorldPos);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        public void Exit()
        {
            ResetHighlighterArea();
        }

        public BuildingStateData GetData()
        {
            return _data;
        }

        private void HighlightArea(Vector3 mouseWorldPos)
        {
            List<Vector3Int> area = _mapModel.GetTilesArea(mouseWorldPos, _data.Building.Square);
            List<Color> colors = area.Select(position => _mapModel.IsBuildable(position) ? Color.green : Color.red).ToList();
            
            _highlighter.HighlightArea(area, colors);
            _lastHighlightedArea = area;
        }

        private void ResetHighlighterArea()
        {
            if (_lastHighlightedArea != null)
            {
                List<Color> colors = _lastHighlightedArea.Select(position => Color.white).ToList();
                _highlighter.HighlightArea(_lastHighlightedArea, colors);
                _lastHighlightedArea = null;
            }
        }

        private void Build(Vector3 mouseWorldPos)
        {
            if (_lastHighlightedArea != null)
            {
                bool isAreaBuildable = !_lastHighlightedArea.Exists(position => !_mapModel.IsBuildable(position));
                if (isAreaBuildable)
                {
                    if (_builder.CanBuild(mouseWorldPos))
                    {
                        GameObject building =_builder.TryBuild(mouseWorldPos, _data.Building.Prefab);

                        AttachBuildingBehavior(building, mouseWorldPos);

                        List<TileBase> tiles = _lastHighlightedArea.Select(position => _mapModel.GetTilesByTemplateName("BuildingFoundationTile").Random()).ToList();
                        _terraformer.ChangeTileArea(_lastHighlightedArea, tiles);
                        _gameStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
                    }
                }
            }
        }

        private void AttachBuildingBehavior(GameObject building, Vector3 position)
        {
            BuildingModel buildingModel = building.AddComponent<BuildingModel>();
            buildingModel.Template = _data.Building;
            buildingModel.Position = _mapModel.Map.WorldToCell(position);
        }
    }
}
