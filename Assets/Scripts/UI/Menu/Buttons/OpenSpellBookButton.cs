using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class OpenSpellBookButton : MonoBehaviour
    {
        public Action OnClick;

        [SerializeField]
        private Button _button;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void HandleClick()
        {
            OnClick?.Invoke();
        }
    }
}
