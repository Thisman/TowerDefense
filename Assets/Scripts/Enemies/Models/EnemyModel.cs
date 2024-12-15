using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField]
    private string _enemyName;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private int _damageAfterDeath;

    private float _health;

    public string EnemyName
    {
        get { return _enemyName; }
        set { _enemyName = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public float MaxHealth { get { return _maxHealth; } }

    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int DamageAfterDeath { get { return _damageAfterDeath; } }
}
