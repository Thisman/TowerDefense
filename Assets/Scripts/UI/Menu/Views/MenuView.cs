using Game.Castle;
using Game.UI;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public class MenuView : MonoBehaviour
{
    public Action OnStateChanged;
    public Action OnSpellBookOpened;
    public Action<GameObject> OnBuildingChosen;

    [SerializeField]
    private GoToNextStateButton _goToNextStateButtonUI;

    [SerializeField]
    private OpenSpellBookButton _openSpellBookButtonUI;

    [SerializeField]
    private ChooseBuildingButton[] _chooseBuildingButtonsUI;

    [Inject]
    private ResourcesModel _resourcesModel;

    private IDisposable _moneyObserver;

    public void OnDestroy()
    {
        _moneyObserver?.Dispose();
    }

    public void OnEnable()
    {
        _moneyObserver = _resourcesModel.Money.Subscribe(HandleMoneyChanged);
        _goToNextStateButtonUI.OnClicked += HandleStateButtonClicked;
        _openSpellBookButtonUI.OnClicked += HandleOpenSpellBookButtonClicked;
        foreach (var button in _chooseBuildingButtonsUI)
        {
            button.OnClicked += HandleBuildingButtonClicked;
        }
    }

    public void OnDisable()
    {
        _goToNextStateButtonUI.OnClicked -= HandleStateButtonClicked;
        _openSpellBookButtonUI.OnClicked -= HandleOpenSpellBookButtonClicked;
        foreach (var button in _chooseBuildingButtonsUI)
        {
            button.OnClicked -= HandleBuildingButtonClicked;
        }
    }

    public void SwitchGoToNextButtonState(bool active)
    {
        if (active)
        {
            _goToNextStateButtonUI.Show();
        } else
        {
            _goToNextStateButtonUI.Hide();
        }
    }

    private void HandleStateButtonClicked()
    {
        OnStateChanged?.Invoke();
    }

    private void HandleOpenSpellBookButtonClicked()
    {
        OnSpellBookOpened?.Invoke();
    }

    private void HandleBuildingButtonClicked(GameObject building)
    {
        OnBuildingChosen?.Invoke(building);
    }

    private void HandleMoneyChanged(int money)
    {
        foreach (var button in _chooseBuildingButtonsUI)
        {
            button.ChangeDisableStatus(money);
        }
    }
}
