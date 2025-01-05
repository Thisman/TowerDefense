using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BuildingView : ViewUI
    {
        [SerializeField]
        private Image _buildingAvatar;

        public void Show(GameObject building)
        {
            _buildingAvatar.sprite = building.GetComponent<SpriteRenderer>().sprite;
            _buildingAvatar.SetNativeSize();

            base.Show();
        }
    }
}
