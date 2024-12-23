using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Map
{
    public class Terraformer
    {
        [Inject]
        private MapModel _mapModel;

        public void ChangeTileArea(List<Vector3Int> area, List<TileBase> tiles)
        {
            for (int i = 0; i < area.Count; i++)
            {
                _mapModel.Map.SetTile(area[i], tiles[i]);
            }
        }
    }
}
