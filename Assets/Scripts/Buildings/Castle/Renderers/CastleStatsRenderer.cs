using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CastleStatsRenderer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerHealthTextUI;

    [SerializeField]
    private TextMeshProUGUI _playerMoneyTextUI;

    [SerializeField]
    private Image _playerHealthIconUI;

    [SerializeField]
    private Image _playerMoneyIconUI;

    [Inject]
    private CastleModel _playerModel;

    [Inject]
    private BuilderBankModel _builderBankModel;

    public void Start()
    {
        _playerHealthIconUI.transform.DOScale(1.1f, 1f).SetEase(Ease.InCirc).SetLoops(-1);
        DOTween.Sequence()
            .Append(_playerMoneyIconUI.transform.DORotate(new Vector3(0, 0, 360), 0.5f))
            .AppendInterval(4)
            .SetLoops(-1);

        HandleBankMoneyChange(_builderBankModel.Money);
        HandlePlayerHealthChange(_playerModel.Health.Value);

        _builderBankModel.OnMoneyChange += HandleBankMoneyChange;
        _playerModel.Health.Subscribe(HandlePlayerHealthChange);
    }

    private void HandlePlayerHealthChange(int health)
    {
        _playerHealthTextUI.text = "Health: " + health.ToString();
    }

    private void HandleBankMoneyChange(int money)
    {
        _playerMoneyTextUI.text = "Money: " + money.ToString();
    }
}
