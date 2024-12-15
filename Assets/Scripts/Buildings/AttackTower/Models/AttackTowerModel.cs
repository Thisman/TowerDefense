using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTowerModel : TowerModel
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _watchRadius = 10f;

    [SerializeField]
    private float _reloadTimeSec = 2;

    public GameObject BulletPrefab { get { return _bulletPrefab; } }

    public float WatchRadius { get { return _watchRadius; } }

    public float ReloadTimeSec { get { return _reloadTimeSec; } }
}
