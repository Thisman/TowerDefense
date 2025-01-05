using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Map
{
    public class MapHighlighter
    {
        [Inject]
        private MapModel mapModel;

        public void HighlightTile(Vector3Int position, Color color)
        {
            if (mapModel.Mask.GetTileFlags(position) != TileFlags.None)
            {
                mapModel.Mask.SetTileFlags(position, TileFlags.None);
            }

            color.a = 0.5f;
            mapModel.Mask.SetColor(position, color);
        }

        public void ResetTileHighlight(Vector3Int position)
        {
            Color color = Color.white;
            color.a = 0;
            mapModel.Mask.SetColor(position, color);
        }

        public void ResetTileHighlight(Vector3 position)
        {
            Vector3Int tilePosition = mapModel.Mask.WorldToCell(position);
            ResetTileHighlight(tilePosition);
        }

        public void HighlightTile(Vector3 position, Color color)
        {
            Vector3Int tilePosition = mapModel.Mask.WorldToCell(position);
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
                Vector3Int tilePosition = mapModel.Mask.WorldToCell(position);
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

        public void ResetAreaHighlight(List<Vector3Int> area)
        {
            for (int i = 0; i < area.Count; i++)
            {
                ResetTileHighlight(area[i]);
            }
        }
    }
}
