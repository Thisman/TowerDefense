using UnityEngine;

namespace Game.Buildings
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField]
        private int _price = 0;

        [SerializeField]
        private int _square = 9;

        [SerializeField]
        private Sprite _avatar;

        [SerializeField]
        [TextArea(3, 10)]
        private string _description;

        public Sprite Avatar => _avatar;

        public int Square => _square;

        public string Description => _description;

        public int Price => _price;
    }
}
