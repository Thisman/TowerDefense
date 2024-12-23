using Game.Buildings;
using Game.Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Castle
{
    public class ShopModel : MonoBehaviour
    {
        [SerializeField]
        public ReactiveCollection<BuildingTemplate> Buildings = new();

        public void Start()
        {
            Buildings.AddRange(Resources.LoadAll<BuildingTemplate>("Templates/Buildings").ToList());
        }
    }
}
