using Game.Buildings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ChooseBuildingButton : MonoBehaviour
    {
        public Action<GameObject> OnClicked;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _buildingPrefab;

        private BuildingModel _buildingModel;

        public void Awake()
        {
            _buildingModel = _buildingPrefab.GetComponent<BuildingModel>();
        }

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void ChangeDisableStatus(int money)
        {
            // TODO: refactoring, for some reasons func invoke early than _buildingModel initiate
            if (_buildingModel != null)
            {
                _button.interactable = money >= _buildingModel.Price;
            }
        }

        private void HandleButtonClicked()
        {
            OnClicked?.Invoke(_buildingPrefab);
        }
    }
}
