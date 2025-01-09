using Game.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Map
{
    public class MapTerraformer
    {
        [Inject]
        private MapModel _mapModel;

        public void HideProps(Vector3Int position)
        {
            _mapModel.PropsLayer.SetTile(position, null);
        }

        public void HidePropsInArea(List<Vector3Int> area)
        {
            foreach(var tilePosition in area)
            {
                HideProps(tilePosition);
            }
        }
    }
}
