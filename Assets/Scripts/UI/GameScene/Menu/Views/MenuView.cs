using Game.Castle;
using Game.UI;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public class MenuView : MonoBehaviour
{
    public Action OnStateChanging;
    public Action OnSpellBookOpening;
    public Action<GameObject> OnTowerChosen;

    [SerializeField]
    private GoToNextStateButton _goToNextStateButtonUI;

    [SerializeField]
    private OpenSpellBookButton _openSpellBookButtonUI;

    [SerializeField]
    private ChooseTowerButton[] _chooseTowerButtonsUI;

    [SerializeField]
    private EnemySpawnPanel _enemySpawnPanelUI;

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
        foreach (var button in _chooseTowerButtonsUI)
        {
            button.OnClicked += HandleChooseTowerButtonClicked;
        }
    }

    public void OnDisable()
    {
        _goToNextStateButtonUI.OnClicked -= HandleStateButtonClicked;
        _openSpellBookButtonUI.OnClicked -= HandleOpenSpellBookButtonClicked;
        foreach (var button in _chooseTowerButtonsUI)
        {
            button.OnClicked -= HandleChooseTowerButtonClicked;
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

    public void UpdateEnemySpawnInfo(bool visible)
    {
        if (visible)
        {
            _enemySpawnPanelUI.Show();
        } else
        {
            _enemySpawnPanelUI.Hide();
        }
    }

    private void HandleStateButtonClicked()
    {
        OnStateChanging?.Invoke();
    }

    private void HandleOpenSpellBookButtonClicked()
    {
        OnSpellBookOpening?.Invoke();
    }

    private void HandleChooseTowerButtonClicked(GameObject tower)
    {
        OnTowerChosen?.Invoke(tower);
    }

    private void HandleMoneyChanged(int money)
    {
        foreach (var button in _chooseTowerButtonsUI)
        {
            button.ChangeDisableStatus(money);
        }
    }
}
