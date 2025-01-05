using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Core
{
    public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image _imageRenderer;

        virtual public void Show()
        {
            DOTween.Sequence()
                .Join(_imageRenderer.DOFade(1, .2f).From(0))
                .Join(gameObject.transform.DOScale(1, .2f).From(0));
        }

        virtual public void Hide()
        {
            OnPointerExit(new PointerEventData(EventSystem.current));
            DOTween.Sequence()
                .Join(_imageRenderer.DOFade(0, .2f).From(1))
                .Join(gameObject.transform.DOScale(0, .2f).From(1));
        }

        public void OnPointerEnter(PointerEventData data)
        {
            DOTween.Sequence()
                .Join(gameObject.transform.DORotate(new Vector3(1, 1, 1.6f), .3f))
                .Join(gameObject.transform.DOScale(new Vector3(1.04f, 1.04f, 1), .3f));
        }

        public void OnPointerExit(PointerEventData data)
        {
            DOTween.Sequence()
                .Join(gameObject.transform.DORotate(new Vector3(0, 0, 0), .3f))
                .Join(gameObject.transform.DOScale(new Vector3(1, 1, 1), .3f));
        }
    }
}
