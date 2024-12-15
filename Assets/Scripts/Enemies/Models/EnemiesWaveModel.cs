using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesWaveModel : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private int _limit;

    [SerializeField]
    private string _waveName;

    [SerializeField]
    private float _spawnDelay;

    public GameObject EnemyPrefab { get { return _enemyPrefab;  } }

    public int Limit {
        get { return _limit; }
        set { _limit = value; }
    }

    public string WaveName { get { return _waveName; } }

    public float SpawnDelay { get { return _spawnDelay; } }
}
