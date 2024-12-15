using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class MapModel
{
    private Tilemap _map;
    private GameObject _enemiesTargetPosition;
    private GameObject _enemiesSpawnPosition;

    private List<TileTemplate> _tilesDataList = new();

    private List<Vector3> _enemiesPath;
    private Dictionary<TileBase, TileTemplate> _tilesDataDictionary = new();

    public MapModel()
    {
        _tilesDataList = Resources.LoadAll<TileTemplate>("Templates/Tiles").ToList();

        foreach (TileTemplate tileData in _tilesDataList)
        {
            foreach (TileBase tile in tileData.Tiles)
            {
                _tilesDataDictionary.Add(tile, tileData);
            }
        }
    }

    public Tilemap Map {
        get { return _map; }
        set { _map = value; }
    }

    public GameObject EnemiesTargetPosition {
        get { return _enemiesSpawnPosition; }
        set { _enemiesSpawnPosition = value;  }
    }

    public GameObject EnemiesSpawnPosition {
        get { return _enemiesTargetPosition; }
        set { _enemiesTargetPosition = value; }
    }

    public List<Vector3> EnemiesPath
    {
        get { return _enemiesPath;  }
        set { _enemiesPath = value; }
    }

    public TileTemplate GetTileData(Vector3Int position)
    {
        TileBase tile = _map.GetTile(position);
        if (tile != null && _tilesDataDictionary.ContainsKey(tile))
        {
            return _tilesDataDictionary[tile];
        }

        return null;
    }

    public List<Vector3Int> GetTilesArea(Vector3Int center, int area)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        int sideLength = Mathf.CeilToInt(Mathf.Sqrt(area));
        int halfSide = sideLength / 2;

        int addedTiles = 0;

        for (int x = -halfSide; x <= halfSide; x++)
        {
            for (int y = -halfSide; y <= halfSide; y++)
            {
                if (addedTiles >= area) return tilePositions;

                Vector3Int position = new Vector3Int(center.x + x, center.y + y, center.z);

                tilePositions.Add(position);
                addedTiles++;
            }
        }

        return tilePositions;
    }

    public bool IsTileAreaAvailableForBuilding(List<Vector3Int> area)
    {
        foreach (Vector3Int position in area)
        {
            if (!IsTileAvailableForBuilding(position))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsTileAvailableForBuilding(Vector3Int position)
    {
        TileBase tile = _map.GetTile(position);
        if (tile == null) {
            return false;
        }

        TileTemplate tileData = _tilesDataDictionary[tile];
        if (tileData != null)
        {
            return tileData.IsAvailableForBuilding;
        }

        return false;
    }

    public bool IsTileWalkable(Vector3Int position)
    {
        TileBase tile = _map.GetTile(position);
        TileTemplate tileData = _tilesDataDictionary[tile];
        if (tileData != null)
        {
            return tileData.IsWalkable;
        }

        return false;
    }
}
