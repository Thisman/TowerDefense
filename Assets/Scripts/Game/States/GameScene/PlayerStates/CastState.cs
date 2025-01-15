using Game.Core;
using Game.Map;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public struct CastStateData
    {
        public GameObject Cast;

        public CastStateData(GameObject cast)
        {
            Cast = cast;
        }
    }

    public class CastState : IState<CastStateData>
    {
        [Inject]
        private MapBuilder _mapBuilder;

        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private CursorController _cursorController;

        private CastStateData _data;
        private GameObject _tempSpell;

        public void Enter() { }

        public void Enter(CastStateData data)
        {
            _data = data;
            _tempSpell = CreateTempSpell();

            _cursorController.SetCursor("magic");
        }

        public void Update()
        {
            if (_tempSpell != null)
            {
                MoveTempSpell();
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleSpellCasted();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        public void Exit()
        {
            _cursorController.SetCursor("default");

            if (_tempSpell != null)
            {
                GameObject.Destroy(_tempSpell);
                _tempSpell = null;
            }
        }

        public CastStateData GetData()
        {
            return _data;
        }

        private void HandleSpellCasted()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            _mapBuilder.CastSpell(position, _data.Cast);
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private GameObject CreateTempSpell()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            return GameObject.Instantiate(_data.Cast, position, Quaternion.identity);
        }

        private void MoveTempSpell()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            _tempSpell.transform.position = position;
        }
    }
}
