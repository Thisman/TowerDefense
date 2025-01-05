using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Map
{
    public class MapModel: MonoBehaviour
    {
        [SerializeField]
        private Tilemap _mask;

        [SerializeField]
        private Tilemap _buildingGround;

        [SerializeField]
        private Tilemap _enemiesRoad;

        [SerializeField]
        private GameObject _castle;

        [SerializeField]
        private GameObject _enemiesSpawn;

        private int _buildingSquare = 9;

        private Dictionary<Vector3, GameObject> _buildings = new();

        public Tilemap Mask => _mask;

        public Tilemap BuildingGround => _buildingGround;

        public GameObject Castle => _castle;

        public GameObject EnemiesSpawn => _enemiesSpawn;

        public int BuildingSquare => _buildingSquare;

        public void Start()
        {
            MakeTilesTransparent();
            _mask.gameObject.SetActive(true);
        }

        public bool ConstructBuilding(Vector3Int position, GameObject building)
        {
            if (_buildings.ContainsKey(position))
            {
                return false;
            }

            List<Vector3Int> constructionArea = GetTilesArea(position, _buildingSquare);
            foreach (var tilePosition in constructionArea)
            {
                _buildings.Add(tilePosition, building);
            }

            return true;
        }

        public void RemoveBuilding(Vector3Int position)
        {
            List<Vector3Int> constructionArea = GetTilesArea(position, _buildingSquare);
            foreach (var tilePosition in constructionArea)
            {
                _buildings.Remove(tilePosition);
            }
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
            Vector3Int tilePosition = _mask.WorldToCell(center);
            return GetTilesArea(tilePosition, area);
        }

        public bool IsAvailableForBuilding(Vector3Int position)
        {
            return _buildingGround.HasTile(position) && !_buildings.ContainsKey(position);
        }

        public bool IsAvailableForBuilding(Vector3 position)
        {
            Vector3Int tilePosition = _mask.WorldToCell(position);
            return IsAvailableForBuilding(tilePosition);
        }

        public bool IsAvailableForWalk(Vector3Int position)
        {
            return _enemiesRoad.HasTile(position);
        }
        
        public Vector3 GetTileCenter(Vector3Int position)
        {
            return _mask.GetCellCenterWorld(position);
        }

        public Vector3 GetTileCenter(Vector3 position)
        {
            Vector3Int tilePosition = _mask.WorldToCell(position);
            return GetTileCenter(tilePosition);
        }

        public GameObject GetBuildingByPosition(Vector3Int position)
        {
            if (_buildings.ContainsKey(position))
            {
                return _buildings[position];
            }

            return null;
        }

        public GameObject GetBuildingByPosition(Vector3 position)
        {
            Vector3Int tilePosition = _mask.WorldToCell(position);
            return GetBuildingByPosition(tilePosition);
        }

        private void MakeTilesTransparent()
        {
            BoundsInt bounds = _mask.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    _mask.SetTileFlags(position, TileFlags.None);
                    if (_mask.HasTile(position))
                    {
                        Color tileColor = _mask.GetColor(position);

                        tileColor.a = 0;
                        _mask.SetColor(position, tileColor);
                    }
                }
            }
        }
    }
}
