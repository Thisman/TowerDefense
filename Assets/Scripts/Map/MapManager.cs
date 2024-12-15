using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Zenject;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap _map;

    [SerializeField]
    private GameObject _enemiesSpawnPoint;

    [SerializeField]
    private GameObject _enemiesTargetPoint;

    [SerializeField]
    private bool _traceEnemiesPath;

    [Inject]
    private MapModel _mapModel;

    [Inject]
    private MapPathFinder _mapPathFinder;

    [Inject]
    private MapHighlighter _mapHighlighter;

    public void Start()
    {
        _mapModel.Map = _map;
        _mapModel.EnemiesSpawnPosition = _enemiesSpawnPoint;
        _mapModel.EnemiesTargetPosition = _enemiesTargetPoint;
        _mapModel.EnemiesPath = _mapPathFinder.GenerateEnemiesPath(_mapModel);

        HighlightEnemiesPath(_mapModel.EnemiesPath);
    }

    private void HighlightEnemiesPath(List<Vector3> path)
    {
        if (_traceEnemiesPath)
        {
            foreach (Vector3 tilePosition in path)
            {
                _mapHighlighter.HighlightTile(_mapModel.Map.WorldToCell(tilePosition), Color.blue);
            }
        }
    }
}
