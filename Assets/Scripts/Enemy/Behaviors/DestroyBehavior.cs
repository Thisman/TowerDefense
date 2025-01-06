using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(EnemyModel))]
    public class DestroyBehavior : MonoBehaviour
    {
        [SerializeField]
        private EnemyModel _enemyModel;

        public void Update()
        {
            if (_enemyModel.Health < 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
