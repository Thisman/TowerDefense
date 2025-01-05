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
            GameObject building = GameObject.Instantiate(prefab, _mapModel.GetTileCenter(tilePosition), Quaternion.identity);
            return _mapModel.ConstructBuilding(tilePosition, building);
        }

        public void RemoveBuilding(Vector3Int position)
        {
            position.z = 0;
            _mapModel.RemoveBuilding(position);
        }

        public void CastSpell(Vector3 position, GameObject spell)
        {
            position.z = 0;
            GameObject.Instantiate(spell, position, Quaternion.identity);
        }
    }
}
