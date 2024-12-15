using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Zenject;

public class MapBuilder
{
    [Inject]
    private DiContainer _diContainer;

    [Inject]
    private MapModel _mapModel;

    private Dictionary<Vector3Int, GameObject> _towersOnMap = new();
    private Dictionary<GameObject, Vector3Int> _towersPosition = new();

    public bool TryBuild(Vector3Int position, List<Vector3Int> area, GameObject prefab, Transform parent)
    {
        if (_mapModel.IsTileAreaAvailableForBuilding(area) && !_towersOnMap.ContainsKey(position))
        {
            GameObject tower = InstantiatePrefab(position, prefab, parent);
            _towersOnMap.Add(position, tower);
            _towersPosition.Add(tower, position);
            return true;
        }

        return false;
    }

    public bool TryDestroy(Vector3Int position)
    {
        if (_towersOnMap.ContainsKey(position))
        {
            GameObject objectOnMap = _towersOnMap[position];
            _towersOnMap.Remove(position);
            _towersPosition.Remove(objectOnMap);
            GameObject.Destroy(objectOnMap);

            return true;
        }

        return false;
    }

    public Vector3Int? GetTowerPosition(GameObject tower)
    {
        if (_towersPosition.ContainsKey(tower))
        {
            return _towersPosition[tower];
        }

        return null;
    }

    public GameObject GetTower(Vector3Int position)
    {
        if (_towersOnMap.ContainsKey(position))
        {
            return _towersOnMap[position];
        }

        return null;
    }

    private GameObject InstantiatePrefab(Vector3Int position, GameObject prefab, Transform parent)
    {
        // Hack for rendering lines. Fix later
        Vector3 cellCenterPosition = _mapModel.Map.GetCellCenterWorld(position) + new Vector3(0, 0, -1);
        GameObject newObject = _diContainer.InstantiatePrefab(prefab, cellCenterPosition, Quaternion.identity, parent);

        return newObject;
    }
}
