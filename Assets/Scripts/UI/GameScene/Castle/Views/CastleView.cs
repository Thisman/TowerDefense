using Game.Castle;
using Game.Core;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class CastleView : ViewUI
    {
        [SerializeField]
        private TextMeshProUGUI _moneyCountTextUI;

        [SerializeField]
        private TextMeshProUGUI _healthCountTextUI;

        [Inject]
        private ResourcesModel _resourcesModel;

        [Inject]
        private CastleModel _castleModel;

        private IDisposable _moneyObserver;
        private IDisposable _castleObserver;

        public void Start()
        {
            _moneyObserver = _resourcesModel.Money.Subscribe(HandleMoneyChanged);
            _castleObserver = _castleModel.Health.Subscribe(HandleCastleHealthChanged);
        }

        public void OnDestroy()
        {
            _moneyObserver.Dispose();
            _castleObserver.Dispose();
        }

        private void HandleMoneyChanged(int money)
        {
            _moneyCountTextUI.text = "Деньги: " + money.ToString();
        }

        private void HandleCastleHealthChanged(int health)
        {
            _healthCountTextUI.text = "Здоровье: " + health.ToString();
        }
    }
}
