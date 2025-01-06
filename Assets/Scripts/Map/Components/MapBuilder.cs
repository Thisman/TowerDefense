using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class MapBuilder
    {
        [Inject]
        private MapModel _mapModel;

        public bool ConstructBuilding(Vector3 position, GameObject prefab)
        {
            position.z = 0;
            Vector3Int tilePosition = _mapModel.Mask.WorldToCell(position);
            GameObject building = GameObject.Instantiate(prefab, _mapModel.GetTileCenter(tilePosition), Quaternion.identity, _mapModel.Castle.transform);
            return _mapModel.ConstructBuilding(tilePosition, building);
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
