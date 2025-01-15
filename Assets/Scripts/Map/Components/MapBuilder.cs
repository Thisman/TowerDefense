using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class MapBuilder
    {
        [Inject]
        private MapModel _mapModel;

        [Inject]
        private MapTerraformer _mapTerraformer;

        public bool ConstructTower(Vector3 position, GameObject tower)
        {
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            List<Vector3Int> constructionArea = _mapModel.GetTilesArea(position, _mapModel.TowerSquare);
            List<Vector3Int> towerConstructionArea = _mapModel.GetTilesArea(position, _mapModel.TowerConstructionArea);

            bool canBuild = constructionArea.FindAll(position => !_mapModel.IsAvailableForBuilding(position)).Count == 0;
            if (!canBuild) { return false; }

            _mapTerraformer.RemovePropsInArea(towerConstructionArea);
            return _mapModel.AddTowerToMap(tilePosition, tower, constructionArea);
        }

        public void RemoveTower(GameObject tower)
        {
            _mapModel.RemoveTowerFromMap(tower);
            GameObject.Destroy(tower);
        }

        // TODO: remove to another place
        public void CastSpell(Vector3 position, GameObject spell)
        {
            position.z = 0;
            GameObject.Instantiate(spell, position, Quaternion.identity);
        }
    }
}
