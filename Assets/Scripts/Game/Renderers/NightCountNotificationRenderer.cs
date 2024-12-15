using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NightCountNotificationRenderer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nightCountNotificationTextUI;

    private Sequence _notificationAnimation;

    public void ShowNotification()
    {
        if (_notificationAnimation != null)
        {
            _notificationAnimation.Kill();
        }

        _nightCountNotificationTextUI.gameObject.SetActive(true);
        _notificationAnimation = DOTween.Sequence()
            .Append(_nightCountNotificationTextUI.DOFade(1, 2f))
            .Append(_nightCountNotificationTextUI.DOFade(0, 2f))
            .OnComplete(() => _nightCountNotificationTextUI.gameObject.SetActive(false));
    }
}
