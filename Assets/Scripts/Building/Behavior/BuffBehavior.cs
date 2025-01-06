using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Buildings
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BuffBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _reloadTimeSec = .3f;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AttackTower"))
            {
                BuildingStatsModel buildingStatsModel = other.GetComponent<BuildingStatsModel>();
                buildingStatsModel.ReloadTimeSec -= _reloadTimeSec;
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AttackTower"))
            {
                BuildingStatsModel buildingStatsModel = other.GetComponent<BuildingStatsModel>();
                buildingStatsModel.ReloadTimeSec += _reloadTimeSec;
            }
        }
    }
}
