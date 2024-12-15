using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private BulletModel _bulletModel;

    private Vector3 _targetPoint;
    private float _randomScaleMinOffset = 1f;
    private float _randomScaleMaxOffset = 2f;

    public void Init(GameObject target)
    {
        _targetPoint = target.transform.position;
    }

    public void Update()
    {
        if(_targetPoint != null)
        {
            MoveTowardsTarget();
        }
    }

    public void Explode()
    {
        transform.position = _targetPoint;
        gameObject.transform.DOScale(Random.Range(_randomScaleMinOffset, _randomScaleMaxOffset), 0.2f)
            .OnComplete(() => Destroy(gameObject));
    }

    private void MoveTowardsTarget()
    {
        float step = _bulletModel.Speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, step);

        if (Vector3.Distance(transform.position, _targetPoint) < 0.01f)
        {
            Explode();
        }
    }
}
