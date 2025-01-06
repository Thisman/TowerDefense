using DG.Tweening;
using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SpellCard : CardUI
    {
        public Action<GameObject> OnClick;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _spell;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleChooseCard);
        }

        override public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void HandleChooseCard()
        {
            OnClick?.Invoke(_spell);
        }
    }
}
