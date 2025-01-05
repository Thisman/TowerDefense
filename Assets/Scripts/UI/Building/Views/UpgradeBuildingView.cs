using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;

namespace Game.UI
{
    public class UpgradeBuildingView : MonoBehaviour
    {
        [SerializeField]
        private Button _reloadCardsButton;

        [SerializeField]
        private UpgradeBuildingCard[] _upgradeCard;

        public void OnEnable()
        {
            _reloadCardsButton.onClick.AddListener(HandleReloadUpgradeCards);
        }

        public void OnDisable()
        {
            _reloadCardsButton.onClick.RemoveAllListeners();
        }

        private void HandleReloadUpgradeCards()
        {
        }
    }
}
