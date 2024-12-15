using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private AttackTowerModel _towerModel;

    [SerializeField]
    private LayerMask _targetObjectsLayer;

    [SerializeField]
    private GameObject _weapon;

    [SerializeField]
    private Color _watchRadiusColor = Color.red;

    private LineRenderer _lineRenderer;
    private int _segments = 50;
    private float _shootTimer = 0;

    public void Start()
    {
        //CreateWatchRadiusCircle();
    }

    public void Update()
    {
        GameObject enemy = CheckForEnemies();
        if (enemy != null)
        {
            _shootTimer -= Time.deltaTime;

            if (_shootTimer <= 0f)
            {
                ShootAtTargetObject(enemy);
                _shootTimer = _towerModel.ReloadTimeSec;
            }
        }
    }

    private GameObject CheckForEnemies()
    {
        Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, _towerModel.WatchRadius, _targetObjectsLayer);

        if (objectsInRadius.Length > 0)
        {
            return objectsInRadius[0].gameObject;
        }

        return null;
    }

    private void CreateWatchRadiusCircle()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();

        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.positionCount = _segments;
        _lineRenderer.startColor = _watchRadiusColor;
        _lineRenderer.endColor = _watchRadiusColor;

        DrawCircle();
    }

    private void DrawCircle()
    {
        float angleStep = 360f / _segments;

        Vector3[] positions = new Vector3[_segments];

        for (int i = 0; i < _segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = Mathf.Cos(angle) * _towerModel.WatchRadius;
            float y = Mathf.Sin(angle) * _towerModel.WatchRadius;
            positions[i] = new Vector3(x, y, 0f);
        }

        _lineRenderer.SetPositions(positions);
    }

    private void ShootAtTargetObject(GameObject target)
    {
        GameObject projectile = Instantiate(_towerModel.BulletPrefab, _weapon.transform.position, Quaternion.identity);
        Vector3 direction = (target.transform.position - transform.position).normalized;

        BulletController bulletController = projectile.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.Init(target);
        }
    }
}
