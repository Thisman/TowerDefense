using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ActionsRenderer : MonoBehaviour
    {
        public Action OnBuildingAction;

        [SerializeField]
        private Button _buildingActionButtonUI;

        public void Start()
        {
            _buildingActionButtonUI.onClick.AddListener(() => OnBuildingAction?.Invoke());
        }

        public void OnDisable()
        {
            _buildingActionButtonUI.onClick.RemoveAllListeners();
        }
    }
}
