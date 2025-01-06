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

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void HandleButtonClicked()
        {
            OnClicked?.Invoke(_buildingPrefab);
        }
    }
}
