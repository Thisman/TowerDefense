using Game.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SpellCard : CardUI
    {
        public Action<GameObject> OnClicked;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _spell;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleCardClicked);
        }

        override public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void HandleCardClicked()
        {
            OnClicked?.Invoke(_spell);
        }
    }
}
