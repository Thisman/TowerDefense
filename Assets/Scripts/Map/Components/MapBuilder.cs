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

        public bool ConstructBuilding(Vector3 position, GameObject building, List<Vector3Int> constructionArea)
        {
            position.z = 0;
            Vector3Int tilePosition = _mapModel.MaskLayer.WorldToCell(position);
            bool canBuild = constructionArea.FindAll(position => !_mapModel.IsAvailableForBuilding(position)).Count == 0;
            if (!canBuild) { return false; }

            List<Vector3Int> buildingDestroyArea = _mapModel.GetTilesArea(position, _mapModel.BuildingDestroySquare);
            _mapTerraformer.HidePropsInArea(buildingDestroyArea);
            return _mapModel.ConstructBuilding(tilePosition, building, constructionArea);
        }

        public void RemoveBuilding(GameObject building)
        {
            _mapModel.RemoveBuilding(building);
            GameObject.Destroy(building);
        }

        public void CastSpell(Vector3 position, GameObject spell)
        {
            position.z = 0;
            GameObject.Instantiate(spell, position, Quaternion.identity);
        }
    }
}
