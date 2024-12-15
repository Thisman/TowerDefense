using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MapTerraformer
{
    [Inject]
    private MapModel _mapModel;

    public void FillArea(List<Vector3Int> area, TileBase tile)
    {
        for(int i = 0; i < area.Count; i++)
        {
            _mapModel.Map.SetTile(area[i], tile);
        }
    }
}
