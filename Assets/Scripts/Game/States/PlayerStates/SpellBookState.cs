using Game.UI;
using Game.Core;
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

            _spellBookView.OnSpellChosen += HandleSpellChosen;
            _spellBookView.OnViewHidden += HandleViewHidden;
            _spellBookView.Show();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleViewHidden();
            }
        }

        public void Exit()
        {
            _spellBookView.OnSpellChosen -= HandleSpellChosen;
            _spellBookView.OnViewHidden -= HandleViewHidden;
            _spellBookView.Hide();
        }

        public SpellBookStateData GetData()
        {
            return _data;
        }

        private void HandleViewHidden()
        {
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleSpellChosen(GameObject spell)
        {
            _playerStates.SwitchState<CastState, CastStateData>(new CastStateData(spell));
        }
    }
}
