using System.Collections;
using System.Collections.Generic;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Map
{
    public class Builder
    {
        [Inject]
        private Game.Map.MapModel _mapModel;

        [Inject]
        private DiContainer _diContainer;

        public GameObject TryBuild(Vector3Int position, GameObject prefab)
        {
            Vector3 tileCenter = _mapModel.Map.GetCellCenterWorld(position);
            GameObject tower = _diContainer.InstantiatePrefab(prefab, tileCenter, Quaternion.identity, _mapModel.Castle.transform);
            if (!_mapModel.TryAddObjectOnMap(position, tower))
            {
                GameObject.Destroy(tower);
                return null;
            }

            return tower;
        }

        public GameObject TryBuild(Vector3 position, GameObject prefab)
        {
            Vector3Int tilePosition = _mapModel.Map.WorldToCell(position);
            return TryBuild(tilePosition, prefab);
        }

        public bool TryRemove(GameObject obj)
        {
            if (_mapModel.TryRemoveObjectFromMap(obj))
            {
                GameObject.Destroy(obj);
                return true;
            }

            return false;
        }

        public bool CanBuild(Vector3Int position)
        {
            return !_mapModel.IsTileBusy(position);
        }

        public bool CanBuild(Vector3 position)
        {
            Vector3Int tilePosition = _mapModel.Map.WorldToCell(position);
            return CanBuild(tilePosition);
        }
    }
}
