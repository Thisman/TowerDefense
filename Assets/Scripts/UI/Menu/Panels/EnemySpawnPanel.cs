using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class EnemySpawnPanel : MonoBehaviour
    {
        [SerializeField]
        private float _animationTime;

        private Tween _animation;

        public void OnDestroy()
        {
            _animation.Kill();
        }

        public void OnDisable()
        {
            _animation.Complete();
        }

        public void Show()
        {
            _animation = gameObject.transform.DOScale(1, _animationTime);
        }

        public void Hide()
        {
            _animation = gameObject.transform.DOScale(0, _animationTime);
        }
    }
}
