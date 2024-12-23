using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Game.Map
{
    public class MapModel : MonoBehaviour
    {
        [SerializeField]
        private Tilemap _baseLayer;

        [SerializeField]
        private List<Tilemap> _layers = new List<Tilemap>();

        [SerializeField]
        private List<TileTemplate> _tiles;

        [SerializeField]
        private GameObject _castle;

        [SerializeField]
        private GameObject _enemiesGate;

        private Dictionary<Vector3Int, GameObject> _objectsOnMap = new();
        private Dictionary<TileBase, TileTemplate> _tilesDictionary = new();
        private Dictionary<string, TileTemplate> _tilesTemplateDictionary = new();

        public void Start()
        {
            _tiles = Resources.LoadAll<TileTemplate>("Templates/Tiles").ToList();
            _layers.AddRange(GetComponentsInChildren<Tilemap>());

            foreach (TileTemplate tileData in _tiles)
            {
                _tilesTemplateDictionary.Add(tileData.name, tileData);
                foreach (TileBase tile in tileData.Tiles)
                {
                    _tilesDictionary.Add(tile, tileData);
                }
            }
        }

        public Tilemap Map { get { return _baseLayer; } }

        public GameObject Castle { get { return _castle; } }

        public Vector3 CastlePosition { get { return _castle.transform.position; } }

        public Vector3 EnemiesGatePosition { get { return _enemiesGate.transform.position; } }

        public bool IsWalkable(TileBase tile)
        {
            if (tile != null && _tilesDictionary.ContainsKey(tile))
            {
                return _tilesDictionary[tile].IsWalkable;
            }

            return false;
        }
        
        public bool IsWalkable(Vector3Int position)
        {
            TileBase tile = _baseLayer.GetTile(position);
            return IsWalkable(tile);
        }

        public bool IsBuildable(TileBase tile)
        {
            if (tile != null && _tilesDictionary.ContainsKey(tile))
            {
                return _tilesDictionary[tile].IsBuildable;
            }

            return false;
        }

        public bool IsBuildable(Vector3Int position)
        {
            TileBase tile = _baseLayer.GetTile(position);
            return IsBuildable(tile);
        }
        
        public bool TryAddObjectOnMap(Vector3Int position, GameObject obj)
        {
            if (!_objectsOnMap.ContainsKey(position))
            {
                Debug.Log(obj);
                _objectsOnMap.Add(position, obj);
                return true;
            }

            return false;
        }

        public bool TryRemoveObjectFromMap(Vector3Int position)
        {
            if (position != null && _objectsOnMap.ContainsKey(position))
            {
                _objectsOnMap.Remove(position);
                return true;
            }

            return false;
        }

        public bool TryRemoveObjectFromMap(GameObject obj)
        {
            Vector3Int? position = FindKeyByValue(_objectsOnMap, obj);
            return TryRemoveObjectFromMap((Vector3Int)position);
        }

        public bool IsTileBusy(Vector3Int position)
        {
            return _objectsOnMap.TryGetValue(position, out var temp);
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

        public List<Vector3Int> GetTilesArea(Vector3 center, int area)
        {
            Vector3Int tilePosition = _baseLayer.WorldToCell(center);
            return GetTilesArea(tilePosition, area);
        }

        public List<TileTemplate> Tiles { get { return _tiles; } }

        public List<TileBase> GetTilesByTemplateName(string name)
        {
            TileTemplate tileTemplate;
            _tilesTemplateDictionary.TryGetValue(name, out tileTemplate);

            return tileTemplate.Tiles.ToList();
        }

        private static TKey? FindKeyByValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue value) where TKey : struct
        {
            foreach (var pair in dictionary)
            {
                Debug.Log(pair.Key);
                Debug.Log(pair.Value);
                Debug.Log(value);
                if (EqualityComparer<TValue>.Default.Equals(pair.Value, value))
                {
                    return pair.Key;
                }
            }
            return null;
        }
    }
}
