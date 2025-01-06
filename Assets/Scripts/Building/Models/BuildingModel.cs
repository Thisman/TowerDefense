using UnityEngine;

namespace Game.Buildings
{
    public class BuildingModel : MonoBehaviour
    {
        [SerializeField]
        private Sprite _avatar;

        [SerializeField]
        private int _square = 9;

        [SerializeField]
        [TextArea(3, 10)]
        private string _description;

        public Sprite Avatar => _avatar;

        public int Square => _square;

        public string Description => _description;
    }
}
