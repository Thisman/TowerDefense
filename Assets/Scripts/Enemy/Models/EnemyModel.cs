using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyModel : MonoBehaviour
    {
        public Action<EnemyModel> OnDestroyEnemy;

        [SerializeField]
        private string _name;

        [SerializeField]
        [TextArea(3, 10)]
        private string _description;

        [SerializeField]
        private Sprite _avatar;

        public string Name => _name;

        public Sprite Avatar => _avatar;

        public string Description => _description;

        public void OnDestroy()
        {
            OnDestroyEnemy?.Invoke(this);
        }
    }
}
