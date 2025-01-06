using Game.UI;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Game.States
{
    public struct SpellBookStateData { }

    public class SpellBookState : IState<SpellBookStateData>
    {
        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private SpellBookView _spellBookView;

        private SpellBookStateData _data;

        public void Enter() { }

        public void Enter(SpellBookStateData data)
        {
            _data = data;

            _spellBookView.OnSpellChoose += HandleChooseSpell;
            _spellBookView.OnViewHidden += HandleHideView;
            _spellBookView.Show();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleHideView();
            }
        }

        public void Exit()
        {
            _spellBookView.OnSpellChoose -= HandleChooseSpell;
            _spellBookView.OnViewHidden -= HandleHideView;
            _spellBookView.Hide();
        }

        public SpellBookStateData GetData()
        {
            return _data;
        }

        private void HandleHideView()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleChooseSpell(GameObject spell)
        {
            _playerStates.SwitchState<CastState, CastStateData>(new CastStateData(spell));
        }
    }
}
