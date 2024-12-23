using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Map
{
    [CreateAssetMenu(fileName = "TileTemplate", menuName = "TowerDefense/Templates/Tile")]
    public class TileTemplate : ScriptableObject
    {
        public bool IsWalkable;
        public bool IsBuildable;

        public List<TileBase> Tiles;
    }
}
