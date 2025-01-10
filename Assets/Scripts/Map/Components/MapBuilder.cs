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

        public bool ConstructBuilding(Vector3 position, GameObject building)
        {
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            List<Vector3Int> constructionArea = _mapModel.GetTilesArea(position, _mapModel.BuildingSquare);
            List<Vector3Int> buildingDestroyArea = _mapModel.GetTilesArea(position, _mapModel.BuildingDestroySquare);

            bool canBuild = constructionArea.FindAll(position => !_mapModel.IsAvailableForBuilding(position)).Count == 0;
            if (!canBuild) { return false; }

            _mapTerraformer.RemovePropsInArea(buildingDestroyArea);
            return _mapModel.AddBuildingToMap(tilePosition, building, constructionArea);
        }

        public void RemoveBuilding(GameObject building)
        {
            _mapModel.RemoveBuildingFromMap(building);
            GameObject.Destroy(building);
        }

        // TODO: remove to another place
        public void CastSpell(Vector3 position, GameObject spell)
        {
            position.z = 0;
            GameObject.Instantiate(spell, position, Quaternion.identity);
        }
    }
}
