using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Zenject.Asteroids;
using Zenject;
using Game.Castle;
using System;
using Game.Buildings;

namespace Game.States
{
    public struct ShopStateData
    {
    }

    public class ShopState : IState<ShopStateData>
    {
        [Inject]
        private FSM gameStates;

        [Inject]
        private ShopRenderer _shopRenderer;

        ShopStateData _data;

        public void Enter() {
        }

        public void Enter(ShopStateData data)
        {
            _data = data;

            _shopRenderer.OnBuildingSelected += HandleSelectBuilding;
            _shopRenderer.Show();
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        public void Exit()
        {
            _shopRenderer.OnBuildingSelected -= HandleSelectBuilding;
            _shopRenderer.Hide();
        }

        public ShopStateData GetData()
        {
            return _data;
        }

        private void HandleSelectBuilding(BuildingTemplate template)
        {
            gameStates.SwitchState<BuildingState, BuildingStateData>(new BuildingStateData(template));
        }
    }
}
