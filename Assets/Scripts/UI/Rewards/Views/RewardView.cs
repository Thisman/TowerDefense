using Game.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RewardView : ViewUI
    {
        public Action OnRewardChosen;

        [SerializeField]
        private RewardCard[] _rewardCards;

        override public void OnEnable()
        {
            foreach(var card in _rewardCards)
            {
                card.gameObject.GetComponent<Button>().onClick.AddListener(HandleCardClicked);
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

        private void HandleCardClicked()
        {
            OnRewardChosen?.Invoke();
        }
    }
}
