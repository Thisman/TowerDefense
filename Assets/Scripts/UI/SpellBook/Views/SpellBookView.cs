using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SpellBookView : ViewUI
    {
        public Action<GameObject> OnSpellChoose;

        [SerializeField]
        private SpellCard[] _spellCards;

        override public void OnEnable()
        {
            base.OnEnable();
            foreach (var card in _spellCards)
            {
                card.OnClick += HandleChooseSpell;
            }
        }

        override public void OnDisable()
        {
            base.OnDisable();
            foreach (var card in _spellCards)
            {
                card.OnClick -= HandleChooseSpell;
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

        private void HandleChooseSpell(GameObject spell)
        {
            OnSpellChoose?.Invoke(spell);
        }
    }
}
