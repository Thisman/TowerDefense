using Game.Buildings;
using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BuildingInfoRenderer : MonoBehaviour
    {
        public Action OnDeleteBuildingButtonClicked;

        [SerializeField]
        private GameObject _buildingPanelUI;

        [SerializeField]
        private Image _buildingIconUI;

        [SerializeField]
        private TextMeshProUGUI _buildingNameTextUI;

        [SerializeField]
        private TextMeshProUGUI _buildingDescriptionTextUI;

        [SerializeField]
        private Button _buildingDeleteButtonUI;

        public void Start()
        {
            _buildingDeleteButtonUI.onClick.AddListener(HandleDeleteBuildingButtonClicked);
        }

        public void Render(BuildingTemplate template)
        {
            _buildingIconUI.sprite = template.Icon;
            _buildingNameTextUI.text = template.Name;
            _buildingDescriptionTextUI.text = template.Description;
        }

        public void Show() {
            _buildingPanelUI.SetActive(true);
        }

        public void Hide() {
            _buildingPanelUI.SetActive(false);
        }

        public void OnDisable()
        {
            _buildingDeleteButtonUI.onClick.RemoveAllListeners();
        }

        private void HandleDeleteBuildingButtonClicked()
        {
            OnDeleteBuildingButtonClicked?.Invoke();
        }
    }
}
