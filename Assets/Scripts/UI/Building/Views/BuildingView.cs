using Game.Buildings;
using Game.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BuildingView : ViewUI
    {
        public Action OnBuildingDeleted;

        [SerializeField]
        private Image _buildingAvatarUI;

        [SerializeField]
        private TextMeshProUGUI _descriptionTextUI;

        [SerializeField]
        private Button _deleteButtonUI;

        override public void OnEnable()
        {
            base.OnEnable();
            _deleteButtonUI.onClick.AddListener(HandleDeleteBuildingButtonClicked);
        }

        override public void OnDisable()
        {
            base.OnDisable();
            _deleteButtonUI.onClick.RemoveAllListeners();
        }

        public void Show(GameObject building)
        {
            BuildingModel buildingModel = building.GetComponent<BuildingModel>();
            _buildingAvatarUI.sprite = buildingModel.Avatar;
            _buildingAvatarUI.SetNativeSize();

            _descriptionTextUI.text = buildingModel.Description;

            base.Show();
        }

        private void HandleDeleteBuildingButtonClicked()
        {
            OnBuildingDeleted?.Invoke();
        }
    }
}
