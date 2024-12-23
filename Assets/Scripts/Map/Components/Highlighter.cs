using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Map
{
    public class Highlighter
    {
        [Inject]
        private MapModel mapModel;

        public void HighlightTile(Vector3Int position, Color color)
        {
            if (mapModel.Map.GetTileFlags(position) != TileFlags.None)
            {
                mapModel.Map.SetTileFlags(position, TileFlags.None);
            }

            mapModel.Map.SetColor(position, color);
        }

        public void HighlightTile(Vector3 position, Color color)
        {
            Vector3Int tilePosition = mapModel.Map.WorldToCell(position);
            HighlightTile(tilePosition, color);
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
                Vector3Int tilePosition = mapModel.Map.WorldToCell(position);
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
    }
}
