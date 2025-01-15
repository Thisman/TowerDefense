using UnityEngine;

namespace Game.Towers
{
    public class TowerModel : MonoBehaviour
    {
        [SerializeField]
        private int _price = 0;

        [SerializeField]
        private Sprite _avatar;

        [SerializeField]
        [TextArea(3, 10)]
        private string _description;

        public Sprite Avatar => _avatar;

        public string Description => _description;

        public int Price => _price;
    }
}
