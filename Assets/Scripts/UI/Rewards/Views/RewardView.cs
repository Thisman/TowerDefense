using DG.Tweening;
using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class RewardView : ViewUI
    {
        public Action OnRewardChoose;

        [SerializeField]
        private RewardCard[] _rewardCards;

        override public void OnEnable()
        {
            foreach(var card in _rewardCards)
            {
                card.gameObject.GetComponent<Button>().onClick.AddListener(HandleChooseReward);
            }
        }

        override public void OnDisable()
        {
            foreach (var card in _rewardCards)
            {
                card.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }

        public override void Show()
        {
            base.Show();

            foreach(var card in _rewardCards)
            {
                card.Show();
            }
        }

        private void HandleChooseReward()
        {
            OnRewardChoose?.Invoke();
        }
    }
}
