using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ChooseBuildingButton : MonoBehaviour
    {
        public Action<GameObject> OnClick;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _buildingPrefab;

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void HandleClick()
        {
            OnClick?.Invoke(_buildingPrefab);
        }
    }
}
