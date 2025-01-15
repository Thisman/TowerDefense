using UnityEngine;

namespace Game.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BuffBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _buffReloadTimeSec = .3f;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AttackTower"))
            {
                TowerStatsModel towerStatsModel = other.GetComponent<TowerStatsModel>();
                towerStatsModel.ReloadTimeSec -= _buffReloadTimeSec;
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AttackTower"))
            {
                TowerStatsModel towerStatsModel = other.GetComponent<TowerStatsModel>();
                towerStatsModel.ReloadTimeSec += _buffReloadTimeSec;
            }
        }
    }
}
