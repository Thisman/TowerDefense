using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class ViewUI : MonoBehaviour
    {
        public Action OnViewHidden;

        [SerializeField]
        private bool _shouldHideByEscape;

        [SerializeField]
        private Button _hideViewButtonUI;

        virtual public void OnEnable()
        {
            if (_hideViewButtonUI != null)
            {
                _hideViewButtonUI.onClick.AddListener(HandleHideViewButtonClicked);
            }
        }

        virtual public void OnDisable()
        {
            if (_hideViewButtonUI != null)
            {
                _hideViewButtonUI.onClick.RemoveAllListeners();
            }
        }

        public void Update()
        {
            if (_shouldHideByEscape && Input.GetKeyDown(KeyCode.Escape))
            {
                HandleHideViewButtonClicked();
            }
        }

        private void HandleHideViewButtonClicked()
        {
            OnViewHidden?.Invoke();
        }

        virtual public void Show()
        {
            gameObject.SetActive(true);
        }

        virtual public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
