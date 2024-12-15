using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MapHighlighter
{
    [Inject]
    private MapModel _mapModel;

    public void HighlightTile(Vector3Int position, Color color)
    {
        // Enable highlight
        if (_mapModel.Map.GetTileFlags(position) != TileFlags.None)
        {
            _mapModel.Map.SetTileFlags(position, TileFlags.None);
        }

        _mapModel.Map.SetColor(position, color);
    }

    public void ResetTileHighlight(Vector3Int position)
    {
        HighlightTile(position, Color.white);
    }

    public void HighlightTileArea(List<Vector3Int> area, List<Color> colors)
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
            HighlightTile(area[i], Color.white);
        }
    }
}
