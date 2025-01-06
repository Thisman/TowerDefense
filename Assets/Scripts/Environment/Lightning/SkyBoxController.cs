using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment
{
    public class SkyBoxController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void SetNightLightning()
        {
            _spriteRenderer.DOFade(.7f, .5f);
        }

        public void SetDayLightning()
        {
            _spriteRenderer.DOFade(0, .5f);
        }
    }
}
