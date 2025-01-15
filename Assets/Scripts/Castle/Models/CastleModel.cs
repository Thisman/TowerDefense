using Game.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Castle
{
    public class CastleModel: MonoBehaviour
    {
        public Action OnCastleDestroy;

        [SerializeField]
        private ReactiveProperty<int> _health = new(50);

        public IReadOnlyReactiveProperty<int> Health => _health;

        public void OnDestroy()
        {
            OnCastleDestroy?.Invoke();
        }

        public void ChangeHealth(int diff)
        {
            _health.Value += diff;
        }
    }
}
