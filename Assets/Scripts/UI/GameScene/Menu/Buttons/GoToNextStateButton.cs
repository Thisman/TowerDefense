using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoToNextStateButton : MonoBehaviour
    {
        public Action OnClicked;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private float _animationTime;

        private Tween _animation;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            _animation?.Kill();
        }

        public void Show() {
            _animation = gameObject.transform.DOScale(1, _animationTime);
            _button.enabled = true;
        }

        public void Hide()
        {
            _button.enabled = false;
            _animation = gameObject.transform.DOScale(0, _animationTime);
        }

        private void HandleButtonClicked()
        {
            OnClicked?.Invoke();
        }
    }
}
