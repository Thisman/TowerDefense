using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferTowerModel : TowerModel
{
    [SerializeField]
    private float _watchRadius = 10f;

    public float WatchRadius { get { return _watchRadius; } }
}
