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

        [Inject]
        private ResourcesModel _resourcesModel;

        private IDisposable _moneyObserver;

        public void Start()
        {
            _moneyObserver = _resourcesModel.Money.Subscribe(HandleMoneyChanged);
        }

        public void OnDestroy()
        {
            _moneyObserver.Dispose();
        }

        private void HandleMoneyChanged(int money)
        {
            _moneyCountTextUI.text = "Δενόγθ: " + money.ToString();
        }
    }
}
