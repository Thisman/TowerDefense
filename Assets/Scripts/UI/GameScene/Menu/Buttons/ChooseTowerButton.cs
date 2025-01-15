using Game.Towers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ChooseTowerButton : MonoBehaviour
    {
        public Action<GameObject> OnClicked;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _towerPrefab;

        private TowerModel _towerModel;

        public void Awake()
        {
            _towerModel = _towerPrefab.GetComponent<TowerModel>();
        }

        public void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void ChangeDisableStatus(int money)
        {
            // TODO: refactoring, for some reasons func invoke early than _towerModel initiate
            if (_towerModel != null)
            {
                _button.interactable = money >= _towerModel.Price;
            }
        }

        private void HandleButtonClicked()
        {
            OnClicked?.Invoke(_towerPrefab);
        }
    }
}
