using Game.Towers;
using Game.Castle;
using Game.Core;
using Game.Map;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public struct ConstructionStateData {
        readonly public GameObject Tower;

        public ConstructionStateData(GameObject tower)
        {
            Tower = tower;
        }
    }

    public class ConstructionState : IState<ConstructionStateData>
    {
        [Inject]
        private MapModel _mapModel;

        [Inject]
        private MapBuilder _mapBuilder;

        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private MapHighlighter _mapHighlighter;

        [Inject]
        private MapTerraformer _mapTerraformer;

        [Inject]
        private CursorController _cursorController;

        [Inject]
        private ResourcesModel _resourcesModel;

        [Inject]
        private DiContainer _diContainer;

        private TowerHighlighter _towerHighlighter;

        private ConstructionStateData _data;

        private GameObject _tempTower;

        public void Enter() { }

        public void Enter(ConstructionStateData data)
        {
            _data = data;
            _cursorController.SetCursor("tower");
            _tempTower = HandleStartConstruction();

            _towerHighlighter = _tempTower.GetComponent<TowerHighlighter>();
            _towerHighlighter.ShowEffectArea();
        }

        public void Update()
        {
            if (_tempTower != null)
            {
                _mapTerraformer.ShowPropsInArea(_tempTower.transform.position);
                _mapHighlighter.ResetAreaHighlightForTower(_tempTower.transform.position);
                MoveTempTower();
                _mapTerraformer.HidePropsInArea(_tempTower.transform.position);
                _mapHighlighter.HighlightAreaForTower(_tempTower.transform.position);
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleConfirmConstruction();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleCancelConstruction();
            }
        }

        public void Exit() {
            _cursorController.SetCursor("default");
            _mapTerraformer.ShowPropsInArea(_tempTower.transform.position);
            _mapHighlighter.ResetAreaHighlightForTower(_tempTower.transform.position);

            _towerHighlighter.HideEffectArea();

            if (_tempTower != null)
            {
                _tempTower = null;
            }
        }

        public ConstructionStateData GetData()
        {
            return _data;
        }

        private void HandleCancelConstruction()
        {
            TowerModel towerModel = _tempTower.GetComponent<TowerModel>();
            _resourcesModel.ChangeMoney(towerModel.Price);
            GameObject.Destroy(_tempTower);
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void HandleConfirmConstruction()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            if (_mapBuilder.ConstructTower(position, _tempTower))
            {
                _tempTower.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
            }
        }

        private GameObject HandleStartConstruction()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            return _diContainer.InstantiatePrefab(_data.Tower, _mapModel.GetTileCenter(tilePosition), Quaternion.identity, _mapModel.Castle.transform);
        }

        private void MoveTempTower()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            _tempTower.transform.position = _mapModel.GetTileCenter(tilePosition);
        }
    }
}
