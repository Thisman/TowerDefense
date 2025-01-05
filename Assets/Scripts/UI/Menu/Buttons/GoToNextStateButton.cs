using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoToNextStateButton : MonoBehaviour
    {
        public Action OnClick;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private float _animationTime;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Show() {
            gameObject.transform.DOScale(1, _animationTime);
            _button.enabled = true;
        }

        public void Hide()
        {
            _button.enabled = false;
            gameObject.transform.DOScale(0, _animationTime);
        }

        private void HandleClick()
        {
            OnClick?.Invoke();
        }
    }
}
