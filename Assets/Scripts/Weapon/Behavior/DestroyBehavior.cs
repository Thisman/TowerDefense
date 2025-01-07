using DG.Tweening;
using Game.Enemies;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(BulletModel))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class DestroyBehavior : MonoBehaviour
    {
        [SerializeField]
        private BulletModel _bulletModel;

        private Tween _animation;
        private float _randomScaleMinOffset = 1f;
        private float _randomScaleMaxOffset = 2f;

        public void OnDestroy()
        {
            _animation?.Kill();
        }

        public void Update()
        {
            if (_bulletModel.TargetEnemy == null)
            {
                Explode();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyModel enemyModel = other.GetComponent<EnemyModel>();
                enemyModel.Health -= _bulletModel.Damage;
                Explode();
            }
        }

        public void Explode()
        {
            if (_animation != null) { return; }

            _animation = gameObject.transform.DOScale(Random.Range(_randomScaleMinOffset, _randomScaleMaxOffset), 0.2f)
                .OnComplete(() => GameObject.Destroy(gameObject));
        }
    }
}
