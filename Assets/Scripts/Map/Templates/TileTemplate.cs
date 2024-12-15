using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileTemplate", menuName = "GameTemplates/Tile")]
public class TileTemplate : ScriptableObject
{
    public TileBase[] Tiles;

    public bool IsWalkable;
    public bool IsAvailableForBuilding;
}
