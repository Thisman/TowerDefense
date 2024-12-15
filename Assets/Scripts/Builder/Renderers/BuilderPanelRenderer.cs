using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class BuilderPanelRenderer : MonoBehaviour
{
    public event Action<TowerModel> OnSelectBuilding;

    [SerializeField]
    private GameObject _builderPanelUI;

    [SerializeField]
    private GameObject _buildingCardPrefab;

    [Inject]
    private BuilderModel _builderModel;

    [Inject]
    private BuilderBankModel _builderBankModel;

    [Inject]
    private GameStateModel _gameStateModel;

    private Sequence _cardAnimation;
    private GameObject _animatedCard;
    private Dictionary<TowerModel, Button> _cardButtonDictionary = new();
    
    public void Start()
    {
        _builderPanelUI.SetActive(false);

        _gameStateModel.State.Subscribe(HandleStateChange);

        _builderBankModel.OnMoneyChange += HandleBankMoneyChanged;

        RenderCards();
    }

    private void RenderCards()
    {
        foreach(TowerModel model in _builderModel.BuildingModels) {
            RenderCard(model);
        }
    }

    private void UpdateCards()
    {
        foreach (TowerModel model in _builderModel.BuildingModels)
        {
            UpdateCardAccessibility(model);
        }
    }

    private void HandleStateChange(GameState state)
    {
        bool isPanelVisible = _builderPanelUI.activeSelf;
        if (state == GameState.SELECT_TOWER && !isPanelVisible)
        {
            if (_animatedCard != null)
            {
                HandlePointerExit(_animatedCard);
                _cardAnimation.Complete();
            }

            _builderPanelUI.SetActive(true);
        }
        else if (state != GameState.SELECT_TOWER && isPanelVisible)
        {
            _builderPanelUI.SetActive(false);
        }
    }

    private void HandleBankMoneyChanged(int money)
    {
        UpdateCards();
    }

    private void RenderCard(TowerModel model)
    {
        GameObject card = Instantiate(_buildingCardPrefab, _builderPanelUI.transform);
        TextMeshProUGUI buildingNameTextUI = card.transform.Find("BuildingNameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI buildingCostTextUI = card.transform.Find("BuildingCostText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI buildingDescriptionTextUI = card.transform.Find("BuildingDescriptionText").GetComponent<TextMeshProUGUI>();

        buildingNameTextUI.text = model.TowerName;
        buildingCostTextUI.text = model.Cost.ToString();
        buildingDescriptionTextUI.text = model.TowerDescription;

        Button cardButton = card.GetComponent<Button>();
        cardButton.onClick.AddListener(() => OnSelectBuilding.Invoke(model));
        _cardButtonDictionary.Add(model, cardButton);
        SubscribeToCardEvents(card);
    }

    private void UpdateCardAccessibility(TowerModel model)
    {
        Button cardButton = _cardButtonDictionary[model];
        if (_cardButtonDictionary.ContainsKey(model) && cardButton.interactable != _builderBankModel.IsEnoughMoney(model.Cost))
        {
            cardButton.interactable = _builderBankModel.IsEnoughMoney(model.Cost);
        }
    }

    private void SubscribeToCardEvents(GameObject card)
    {
        // Add EventTrigger component
        EventTrigger trigger = card.AddComponent<EventTrigger>();

        // Create pointer enter event
        EventTrigger.Entry pointerEnterEvent = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnterEvent.callback.AddListener((_) => HandlePointerEnter(card));

        // Create pointer exit event
        EventTrigger.Entry pointerExitEvent = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExitEvent.callback.AddListener((_) => HandlePointerExit(card));

        // Add events to EventTrigger
        trigger.triggers.Add(pointerEnterEvent);
        trigger.triggers.Add(pointerExitEvent);
    }

    private void HandlePointerEnter(GameObject card)
    {
        _animatedCard = card;
        _cardAnimation = DOTween.Sequence();
        _cardAnimation
            .Join(_animatedCard.transform.DOScale(1.2f, 0.3f))
            .Join(_animatedCard.transform.DORotate(new Vector3(0, 0, 5), 0.3f));
    }

    private void HandlePointerExit(GameObject card)
    {
        _animatedCard = card;
        _cardAnimation = DOTween.Sequence();
        _cardAnimation
            .Join(_animatedCard.transform.DOScale(1f, 0.3f))
            .Join(_animatedCard.transform.DORotate(new Vector3(0, 0, 0), 0.3f));
    }
}
