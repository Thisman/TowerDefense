using System;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyModel : MonoBehaviour
    {
        public Action<EnemyModel> OnEnemyDestroyed;

        [SerializeField]
        private string _name;

        [SerializeField]
        [TextArea(3, 10)]
        private string _description;

        [SerializeField]
        private float _health;

        [SerializeField]
        private Sprite _avatar;

        public string Name => _name;

        public Sprite Avatar => _avatar;

        public string Description => _description;

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public void OnDestroy()
        {
            OnEnemyDestroyed?.Invoke(this);
        }
    }
}
