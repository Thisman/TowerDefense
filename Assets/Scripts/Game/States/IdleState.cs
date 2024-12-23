using Game.Core;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public struct IdleStateData
    {
    }

    public class IdleState : IState<IdleStateData>
    {
        [Inject]
        private FSM _gameStates;

        [Inject]
        private ActionsRenderer _actionRenderers;

        private IdleStateData _data;

        public void Enter() { }

        public void Enter(IdleStateData data) {
            _data = data;

            _actionRenderers.OnBuildingAction += HandleBuildingAction;
        }

        public void Update() {
            if (Input.GetMouseButtonDown(0))
            {
                HandleShowBuildingInfo();
            }
        }

        public void Exit() {
        }

        public IdleStateData GetData()
        {
            return _data;
        }

        private void HandleBuildingAction()
        {
            _gameStates.SwitchState<ShopState, ShopStateData>(new ShopStateData());
        }

        private void HandleShowBuildingInfo()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            if (IsBuilding(mousePosition))
            {
                _gameStates.SwitchState<BuildingInfoState, BuildingInfoStateData>(new BuildingInfoStateData(hit.gameObject));
            }
        }

        private bool IsBuilding(Vector3 position)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            return hit != null && hit.gameObject.CompareTag("Building");
        }
    }
}
