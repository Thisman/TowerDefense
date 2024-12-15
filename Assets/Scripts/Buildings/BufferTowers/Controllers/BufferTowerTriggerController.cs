using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferTowerTriggerController : MonoBehaviour
{
    [SerializeField]
    BufferTowerModel _bufferTowerModel;

    public void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = _bufferTowerModel.WatchRadius;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter object");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyModel model = collision.gameObject.GetComponent<EnemyModel>();
            model.Speed = model.Speed / 2;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit object");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyModel model = collision.gameObject.GetComponent<EnemyModel>();
            model.Speed = model.Speed * 2;
        }
    }
}
