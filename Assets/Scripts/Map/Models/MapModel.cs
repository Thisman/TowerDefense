using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Map
{
    public class MapModel: MonoBehaviour
    {
        [SerializeField]
        private Tilemap _maskLayer;

        [SerializeField]
        private Tilemap _propsLayer;

        [SerializeField]
        private Tilemap _buildingLayer;

        [SerializeField]
        private Tilemap _roadLayer;

        [SerializeField]
        private GameObject _castle;

        [SerializeField]
        private GameObject _enemiesSpawn;

        private List<Vector3> _enemiesPath;

        private int _buildingSquare = 9;

        private int _buildingDestroySquare = 25;

        private Dictionary<Vector3, GameObject> _buildingPositions = new();

        public Tilemap MaskLayer => _maskLayer;

        public Tilemap BuildingLayer => _buildingLayer;

        public Tilemap PropsLayer => _propsLayer;

        public GameObject Castle => _castle;

        public GameObject EnemiesSpawn => _enemiesSpawn;

        public int BuildingSquare => _buildingSquare;

        public int BuildingDestroySquare => _buildingDestroySquare;

        public List<Vector3> EnemiesPath
        {
            get { return _enemiesPath; }
            set { _enemiesPath = value; }
        }

        public void Start()
        {
            MakeTilesTransparent();
            _maskLayer.gameObject.SetActive(true);
        }

        public bool AddBuildingToMap(Vector3Int position, GameObject building, List<Vector3Int> constructionArea)
        {
            if (_buildingPositions.ContainsKey(position))
            {
                return false;
            }

            foreach (var tilePosition in constructionArea)
            {
                _buildingPositions.Add(tilePosition, building);
            }

            return true;
        }

        public void RemoveBuildingFromMap(GameObject building)
        {
            _buildingPositions = _buildingPositions
                .Where(el => el.Value != building)
                .ToDictionary(el => el.Key, el => el.Value);
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
            Vector3Int tilePosition = _maskLayer.WorldToCell(center);
            return GetTilesArea(tilePosition, area);
        }

        public bool IsAvailableForBuilding(Vector3Int position)
        {
            return _buildingLayer.HasTile(position) && !_buildingPositions.ContainsKey(position);
        }

        public bool IsAvailableForBuilding(Vector3 position)
        {
            Vector3Int tilePosition = _maskLayer.WorldToCell(position);
            return IsAvailableForBuilding(tilePosition);
        }

        public bool IsAvailableForWalk(Vector3Int position)
        {
            return _roadLayer.HasTile(position);
        }
        
        public Vector3 GetTileCenter(Vector3Int position)
        {
            return _maskLayer.GetCellCenterWorld(position);
        }

        public Vector3 GetTileCenter(Vector3 position)
        {
            Vector3Int tilePosition = _maskLayer.WorldToCell(position);
            return GetTileCenter(tilePosition);
        }

        public GameObject GetBuildingByPosition(Vector3Int position)
        {
            if (_buildingPositions.ContainsKey(position))
            {
                return _buildingPositions[position];
            }

            return null;
        }

        public GameObject GetBuildingByPosition(Vector3 position)
        {
            Vector3Int tilePosition = _maskLayer.WorldToCell(position);
            return GetBuildingByPosition(tilePosition);
        }

        private void MakeTilesTransparent()
        {
            BoundsInt bounds = _maskLayer.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    _maskLayer.SetTileFlags(position, TileFlags.None);
                    if (_maskLayer.HasTile(position))
                    {
                        Color tileColor = _maskLayer.GetColor(position);

                        tileColor.a = 0;
                        _maskLayer.SetColor(position, tileColor);
                    }
                }
            }
        }
    }
}
