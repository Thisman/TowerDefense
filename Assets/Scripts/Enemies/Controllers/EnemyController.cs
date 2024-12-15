using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyModel _enemyModel;

    [SerializeField]
    private float _pathPathOffset = 0.5f;

    [Inject]
    private MapModel _mapModel;

    [Inject]
    private CastleModel _castleModel;

    private SpriteRenderer _spriteRenderer;
    private int _currentPointIndexInPath = 0;
    private float _randomXPathOffset;
    private float _randomYPathOffset;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyModel.Health = _enemyModel.MaxHealth;

        _randomXPathOffset = Random.Range(-_pathPathOffset, _pathPathOffset);
        _randomYPathOffset = Random.Range(-_pathPathOffset, _pathPathOffset);
    }

    public void Update()
    {
        if (IsEnemyDie())
        {
            StopMoving();
            return;
        }

        HighlightEnemy();

        if (_mapModel != null && _mapModel.EnemiesPath != null && _mapModel.EnemiesPath.Count > 0)
        {
            MoveToTarget();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletModel bulletModel = collision.gameObject.GetComponent<BulletModel>();
            BulletController bulletController = collision.gameObject.GetComponent<BulletController>();
            if (bulletModel != null)
            {
                _enemyModel.Health -= bulletModel.Damage;
                bulletController.Explode();
            }
        }

        if (collision.gameObject.CompareTag("PlayerBase"))
        {
            if (_castleModel != null)
            {
                _castleModel.Health.Value -= _enemyModel.DamageAfterDeath;
                StopMoving();
            }
        }
    }

    public void StopMoving()
    {
        Destroy(gameObject);
    }

    private bool IsEnemyDie()
    {
        return _enemyModel.Health < 0;
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = _mapModel.EnemiesPath[_currentPointIndexInPath] + new Vector3(_randomXPathOffset, _randomYPathOffset, 0);
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the object
        transform.position += _enemyModel.Speed * Time.deltaTime * direction;

        // Check if the object is close enough to the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Snap to the exact target position
            transform.position = targetPosition;

            // Move to the next target
            _currentPointIndexInPath++;

            if (_currentPointIndexInPath >= _mapModel.EnemiesPath.Count)
            {

                StopMoving();
            }
        }
    }

    private void HighlightEnemy()
    {
        if (_spriteRenderer != null)
        {
            Color color = GetColorByPercentage((_enemyModel.Health * 100) / _enemyModel.MaxHealth);
            _spriteRenderer.color = color;
        }

    }

    private Color GetColorByPercentage(float percentage)
    {
        percentage = Mathf.Clamp(percentage, 0f, 100f);

        float intensity = percentage / 100f;

        return Color.Lerp(Color.red, Color.white, intensity);
    }
}
