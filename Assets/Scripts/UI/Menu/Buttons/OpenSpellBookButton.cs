using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class OpenSpellBookButton : MonoBehaviour
    {
        public Action OnClicked;

        [SerializeField]
        private Button _button;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void HandleButtonClicked()
        {
            OnClicked?.Invoke();
        }
    }
}
