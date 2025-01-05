using Game.Core;
using Game.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.States
{
    public struct IdleStateData { }

    public class IdleState : IState<IdleStateData>
    {
        [Inject]
        private MapModel _mapModel;

        [Inject]
        private PlayerFSM _playerStates;

        private IdleStateData _data;

        public void Enter() { }

        public void Enter(IdleStateData data)
        {
            _data = data;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleChooseTower();
                HandleChooseEnemy();
            }
        }

        public void Exit()
        {
        }

        public IdleStateData GetData()
        {
            return _data;
        }

        private void HandleChooseTower()
        {
            if (IsPointerOverUIElement())
            {
                return;
            }

            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            GameObject building = _mapModel.GetBuildingByPosition(position);

            if (building)
            {
                _playerStates.SwitchState<BuildingInfoState, BuildingInfoStateData>(new BuildingInfoStateData(building));
            }
        }

        private void HandleChooseEnemy()
        {
            if (IsPointerOverUIElement())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject target = hit.collider.gameObject;
                if (target.CompareTag("Enemy"))
                {
                    _playerStates.SwitchState<EnemyInfoState, EnemyInfoStateData>(new EnemyInfoStateData(target));
                }
            }
        }
        private bool IsPointerOverUIElement()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}

