using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _damage;

    public float Speed { get { return _speed; } }

    public float Damage { get { return _damage; } }
}
