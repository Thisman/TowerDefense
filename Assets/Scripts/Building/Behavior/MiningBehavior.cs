using Game.Buildings;
using Game.Castle;
using Game.Core;
using Game.States;
using UnityEngine;
using Zenject;

namespace Game.Building
{
    [RequireComponent(typeof(BuildingStatsModel))]
    public class MiningBehavior : MonoBehaviour
    {
        [SerializeField]
        private BuildingStatsModel _buildingStatsModel;

        [SerializeField]
        private int _miningMoneyCount;

        [Inject]
        private ResourcesModel _resourcesModel;

        [Inject]
        private GameFSM _gameStates;

        public void Start()
        {
            _gameStates.OnStateChanged += HandleStateChanged;
        }

        public void OnDestroy()
        {
            _gameStates.OnStateChanged -= HandleStateChanged;
        }

        private void HandleStateChanged(IState state)
        {
            Debug.Log(state);
            Debug.Log(state is DayState);
            Debug.Log(state.GetType() == typeof(DayState));
            if (state is DayState)
            {
                MiningMoney();
            }
        }

        private void MiningMoney()
        {
            _resourcesModel.ChangeMoney(_miningMoneyCount);
        }
    }
}
