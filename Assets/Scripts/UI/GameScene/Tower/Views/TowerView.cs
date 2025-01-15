using Game.Towers;
using Game.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class TowerView : ViewUI
    {
        public Action OnTowerDeleting;

        [SerializeField]
        private Image _towerAvatarUI;

        [SerializeField]
        private TextMeshProUGUI _descriptionTextUI;

        [SerializeField]
        private Button _deleteButtonUI;

        override public void OnEnable()
        {
            base.OnEnable();
            _deleteButtonUI.onClick.AddListener(HandleDeleteTowerButtonClicked);
        }

        override public void OnDisable()
        {
            base.OnDisable();
            _deleteButtonUI.onClick.RemoveAllListeners();
        }

        public void Show(GameObject tower)
        {
            TowerModel towerModel = tower.GetComponent<TowerModel>();
            _towerAvatarUI.sprite = towerModel.Avatar;
            _towerAvatarUI.SetNativeSize();

            _descriptionTextUI.text = towerModel.Description;

            base.Show();
        }

        private void HandleDeleteTowerButtonClicked()
        {
            OnTowerDeleting?.Invoke();
        }
    }
}
