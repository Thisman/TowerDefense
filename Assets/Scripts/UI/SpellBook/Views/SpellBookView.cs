using Game.Core;
using System;
using UnityEngine;

namespace Game.UI
{
    public class SpellBookView : ViewUI
    {
        public Action<GameObject> OnSpellChosen;

        [SerializeField]
        private SpellCard[] _spellCards;

        override public void OnEnable()
        {
            base.OnEnable();
            foreach (var card in _spellCards)
            {
                card.OnClicked += HandleCardClicked;
            }
        }

        override public void OnDisable()
        {
            base.OnDisable();
            foreach (var card in _spellCards)
            {
                card.OnClicked -= HandleCardClicked;
            }
        }

        override public void Show()
        {
            base.Show();
            foreach (var card in _spellCards)
            {
                card.Show();
            }
        }

        override public void Hide()
        {
            foreach (var card in _spellCards)
            {
                card.Hide();
            }

            base.Hide();
        }

        private void HandleCardClicked(GameObject spell)
        {
            OnSpellChosen?.Invoke(spell);
        }
    }
}
