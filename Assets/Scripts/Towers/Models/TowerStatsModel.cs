using UnityEngine;

namespace Game.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TowerStatsModel : MonoBehaviour
    {
        [SerializeField]
        private float _reloadTimeSec;

        [SerializeField]
        private float _effectArea;

        [SerializeField] 
        private CircleCollider2D _circleCollider2d;

        public void Start()
        {
            _circleCollider2d.radius = _effectArea;
        }

        public float ReloadTimeSec
        {
            get { return _reloadTimeSec; }
            set { _reloadTimeSec = value; }
        }

        public float EffectArea
        {
            get { return _effectArea; }
            set { 
                _effectArea = value;
                _circleCollider2d.radius = _effectArea;
            }
        }
    }
}
