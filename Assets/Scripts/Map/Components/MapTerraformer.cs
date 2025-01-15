using Game.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Map
{
    public class MapTerraformer
    {
        [Inject]
        private MapModel _mapModel;

        public void RemoveProps(Vector3Int position)
        {
            if (_mapModel.PropsLayer.GetTile(position) != null)
            {
                _mapModel.PropsLayer.SetTile(position, null);
            }
        }

        public void RemovePropsInArea(List<Vector3Int> area) {
            foreach (var tilePosition in area)
            {
                RemoveProps(tilePosition);
            }
        }

        public void HideProps(Vector3Int position)
        {
            if (_mapModel.PropsLayer.GetTile(position) != null)
            {
                if (_mapModel.PropsLayer.GetTileFlags(position) != TileFlags.None)
                {
                    _mapModel.PropsLayer.SetTileFlags(position, TileFlags.None);
                }

                Color color = _mapModel.PropsLayer.GetColor(position);
                color.a = 0;
                _mapModel.PropsLayer.SetColor(position, color);
            }
        }

        public void ShowProps(Vector3Int position)
        {
            if (_mapModel.PropsLayer.GetTile(position) != null)
            {
                Color color = _mapModel.PropsLayer.GetColor(position);
                color.a = 1;
                _mapModel.PropsLayer.SetColor(position, color);
            }
        }

        public void HidePropsInArea(List<Vector3Int> area)
        {
            foreach(var tilePosition in area)
            {
                HideProps(tilePosition);
            }
        }

        public void HidePropsInArea(Vector3 center)
        {
            List<Vector3Int> area = _mapModel.GetTilesArea(center, _mapModel.TowerConstructionArea);
            HidePropsInArea(area);
        }

        public void ShowPropsInArea(List<Vector3Int> area)
        {
            foreach (var tilePosition in area)
            {
                ShowProps(tilePosition);
            }
        }

        public void ShowPropsInArea(Vector3 center)
        {
            List<Vector3Int> area = _mapModel.GetTilesArea(center, _mapModel.TowerConstructionArea);
            ShowPropsInArea(area);
        }
    }
}
