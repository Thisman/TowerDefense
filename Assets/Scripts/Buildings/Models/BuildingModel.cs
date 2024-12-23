using Game.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Building
{
    public class BuildingModel: MonoBehaviour
    {
        [SerializeField]
        private BuildingTemplate _template;

        [SerializeField]
        private Vector3Int _position;


        public BuildingTemplate Template
        {
            get { return _template; }
            set { _template = value; }
        }

        public Vector3Int Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
