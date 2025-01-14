using Game.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Castle
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DestroyBehavior : MonoBehaviour
    {
        [Inject]
        private CastleModel _castleModel;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyModel enemyModel = other.gameObject.GetComponent<EnemyModel>();
                enemyModel.Health = 0;
                _castleModel.Health.Value -= 1;
            }
        }
    }
}
