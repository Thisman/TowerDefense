using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(BulletModel))]
    public class MoveBehavior : MonoBehaviour
    {
        [SerializeField]
        private BulletModel _bulletModel;

        void Update()
        {
            if (_bulletModel.TargetEnemy != null)
            {
                MoveTowardsTarget();
            }
        }

        private void MoveTowardsTarget()
        {
            float step = _bulletModel.Speed * Time.deltaTime;

            Vector3 enemyPosition = _bulletModel.TargetEnemy.gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, enemyPosition, step);
        }
    }
}
