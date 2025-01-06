using Game.UI;
using System;
using UnityEngine;

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

    public void OnEnable()
    {
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
}
