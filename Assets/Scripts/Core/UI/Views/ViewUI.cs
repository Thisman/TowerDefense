using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class ViewUI : MonoBehaviour
    {
        public Action OnHideView;

        [SerializeField]
        private bool _hideByEscape;

        [SerializeField]
        private Button _hideViewButtonUI;

        virtual public void OnEnable()
        {
            if (_hideViewButtonUI != null)
            {
                _hideViewButtonUI.onClick.AddListener(HandleHideView);
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
            if (_hideByEscape && Input.GetKeyDown(KeyCode.Escape))
            {
                HandleHideView();
            }
        }

        private void HandleHideView()
        {
            OnHideView?.Invoke();
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
