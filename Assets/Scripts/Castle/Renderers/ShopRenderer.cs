using Game.Buildings;
using Game.Core;
using Game.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Castle
{
    public class ShopRenderer : MonoBehaviour
    {
        public Action<BuildingTemplate> OnBuildingSelected;

        [SerializeField]
        private GameObject _shopPanelUI;

        [SerializeField]
        private GameObject _buildingCardPrefab;

        [Inject]
        private ShopModel _shopModel;

        public void Start()
        {
            _shopModel.Buildings.ObserveCountChanged().Subscribe(HandleCardChanged);
        }

        public void OnDisable()
        {
        }

        public void Show()
        {
            _shopPanelUI.SetActive(true);
        }

        public void Hide()
        {
            _shopPanelUI.SetActive(false);
        }

        private void HandleCardClicked(BuildingTemplate template)
        {
            OnBuildingSelected?.Invoke(template);
        }

        private void HandleCardChanged(int count)
        {
            _shopPanelUI.transform.DetachChildren();

            foreach (var buildingTemplate in _shopModel.Buildings)
            {
                GameObject card = GameObject.Instantiate(_buildingCardPrefab, _shopPanelUI.transform);
                BuildingTemplateRenderer cardRenderer = card.GetComponent<BuildingTemplateRenderer>();
                cardRenderer.Render(buildingTemplate);
                cardRenderer.OnClicked += HandleCardClicked;
            }
        }
    }
}
