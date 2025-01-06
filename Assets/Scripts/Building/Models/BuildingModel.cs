using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Building
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField]
        private Sprite _avatar;

        [SerializeField]
        [TextArea(3, 10)]
        private string _description;

        [SerializeField]
        private int _square = 9;

        public Sprite Avatar => _avatar;

        public string Description => _description;

        public int Square => _square;
    }
}
