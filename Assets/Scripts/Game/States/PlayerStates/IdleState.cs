using Game.Buildings;
using Game.Castle;
using Game.Core;
using Game.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
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

        [Inject]
        private CursorController _cursorController;

        private IdleStateData _data;

        public void Enter() { }

        public void Enter(IdleStateData data)
        {
            _data = data;
        }

        public void Update()
        {
            if (!IsPointerOverUIElement())
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject hoveredBuilding = GetTowerByMousePosition(mousePosition);
                GameObject hoveredEnemy = GetEnemyByMousePosition(mousePosition);

                ChangeCursor(hoveredBuilding != null || hoveredEnemy != null);

                if (Input.GetMouseButtonDown(0))
                {
                    if (hoveredBuilding != null)
                    {
                        HandleTowerClicked(hoveredBuilding);
                    } else if (hoveredEnemy != null)
                    {
                        HandleEnemyClicked(hoveredEnemy);
                    }
                }
            }
        }

        public void Exit() {
            ChangeCursor(false);
        }

        public IdleStateData GetData()
        {
            return _data;
        }

        private void HandleTowerClicked(GameObject building)
        {
            _playerStates.SwitchState<BuildingInfoState, BuildingInfoStateData>(new BuildingInfoStateData(building));
        }

        private void HandleEnemyClicked(GameObject enemy)
        {
            _playerStates.SwitchState<EnemyInfoState, EnemyInfoStateData>(new EnemyInfoStateData(enemy));
        }

        private GameObject GetEnemyByMousePosition(Vector2 position)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject target = hit.collider.gameObject;
                if (target.CompareTag("Enemy"))
                {
                    return target;
                }
            }

            return null;
        }

        private GameObject GetTowerByMousePosition(Vector2 position)
        {
            return _mapModel.GetBuildingByPosition(position);
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

        private void ChangeCursor(bool isTowerOrEnemyHovered)
        {
            if (isTowerOrEnemyHovered)
            {
                _cursorController.SetCursor("info");
            } else
            {
                _cursorController.SetCursor("default");
            }
        }
    }
}

