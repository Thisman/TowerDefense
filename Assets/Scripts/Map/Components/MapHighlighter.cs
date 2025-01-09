using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Map
{
    public class MapHighlighter
    {
        [Inject]
        private MapModel _mapModel;

        private Color _availableForBuildingColor = Color.green;
        private Color _unAvailableForBuildingColor = Color.red;

        public void HighlightTile(Vector3Int position, Color color)
        {
            if (_mapModel.MaskLayer.GetTileFlags(position) != TileFlags.None)
            {
                _mapModel.MaskLayer.SetTileFlags(position, TileFlags.None);
            }

            color.a = 0.8f;
            _mapModel.MaskLayer.SetColor(position, color);
        }

        public void HighlightTile(Vector3 position, Color color)
        {
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            HighlightTile(tilePosition, color);
        }

        public void ResetTileHighlight(Vector3Int position)
        {
            Color color = Color.white;
            color.a = 0;
            _mapModel.MaskLayer.SetColor(position, color);
        }

        public void ResetTileHighlight(Vector3 position)
        {
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            ResetTileHighlight(tilePosition);
        }

        public void HighlightPath(List<Vector3Int> path, Color color)
        {
            foreach (var position in path)
            {
                HighlightTile(position, color);
            }
        }

        public void HighlightPath(List<Vector3> path, Color color)
        {
            foreach (var position in path)
            {
                Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
                HighlightTile(tilePosition, color);
            }
        }

        public void HighlightArea(List<Vector3Int> area, List<Color> colors)
        {
            for (int i = 0; i < area.Count; i++)
            {
                HighlightTile(area[i], colors[i]);
            }
        }

        public void HighlightAreaForBuilding(Vector3Int center)
        {
            List<Vector3Int> highlightedArea = _mapModel.GetTilesArea(center, _mapModel.BuildingDestroySquare);
            List<Color> colors = GetTilesColor(highlightedArea);
            HighlightArea(highlightedArea, colors);
        }

        public void HighlightAreaForBuilding(Vector3 center)
        {
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(center);
            HighlightAreaForBuilding(tilePosition);
        }

        public void ResetAreaHighlight(List<Vector3Int> area)
        {
            for (int i = 0; i < area.Count; i++)
            {
                ResetTileHighlight(area[i]);
            }
        }

        public void ResetAreaHighlightForBuilding(Vector3Int center)
        {
            List<Vector3Int> highlightedArea = _mapModel.GetTilesArea(center, _mapModel.BuildingDestroySquare);
            ResetAreaHighlight(highlightedArea);
        }

        public void ResetAreaHighlightForBuilding(Vector3 center)
        {
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(center);
            ResetAreaHighlightForBuilding(tilePosition);
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
