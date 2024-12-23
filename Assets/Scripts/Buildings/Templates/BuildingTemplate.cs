using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Buildings
{
    [CreateAssetMenu(fileName = "BuildingTemplate", menuName = "TowerDefense/Templates/Building")]
    public class BuildingTemplate : ScriptableObject
    {
        public int Cost;
        public int Square;
        public Sprite Icon;
        public string Name;
        public string Description;

        public GameObject Prefab;
    }
}
