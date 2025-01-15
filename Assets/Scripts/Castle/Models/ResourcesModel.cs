using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Castle
{
    public class ResourcesModel: MonoBehaviour
    {
        [SerializeField]
        private ReactiveProperty<int> _money = new(100);

        public IReadOnlyReactiveProperty<int> Money => _money;

        public bool ChangeMoney(int moneyDiff)
        {
            if (_money.Value + moneyDiff < 0)
            {
                return false;
            }

            _money.Value += moneyDiff;

            return true;
        }
    }
}
