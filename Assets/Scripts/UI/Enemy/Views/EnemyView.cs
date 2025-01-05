using Game.Core;
using Game.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class EnemyView : ViewUI
    {
        [SerializeField]
        private Image _enemyAvatarUI;

        [SerializeField]
        private TextMeshProUGUI _descriptionTextUI;

        public void Show(GameObject enemy)
        {
            EnemyModel enemyModel = enemy.GetComponent<EnemyModel>();
            _enemyAvatarUI.sprite = enemyModel.Avatar;
            _enemyAvatarUI.SetNativeSize();

            _descriptionTextUI.text = enemyModel.Description;

            base.Show();
        }
    }
}
