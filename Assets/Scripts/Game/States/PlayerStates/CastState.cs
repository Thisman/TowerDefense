using Game.Core;
using Game.Map;
using System.Collections;
using System.Collections.Generic;
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

        private CastStateData _data;
        private GameObject _tempSpell;

        public void Enter() { }

        public void Enter(CastStateData data)
        {
            _data = data;
            _tempSpell = CreateTempSpell();
        }

        public void Update()
        {
            if (_tempSpell != null)
            {
                MoveTempSpell();
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleCastSpell();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        public void Exit()
        {
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

        private void HandleCastSpell()
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
