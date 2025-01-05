using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public Action GoToNextState;
    public Action OpenSpellBook;
    public Action<GameObject> ChooseBuilding;

    [SerializeField]
    private GoToNextStateButton _goToNextStateButtonUI;

    [SerializeField]
    private OpenSpellBookButton _openSpellBookButtonUI;

    [SerializeField]
    private ChooseBuildingButton[] _chooseBuildingButtonsUI;

    public void OnEnable()
    {
        _goToNextStateButtonUI.OnClick += HandleGoToNight;
        _openSpellBookButtonUI.OnClick += HandleOpenSpellBook;
        foreach (var button in _chooseBuildingButtonsUI)
        {
            button.OnClick += HandleChooseBuilding;
        }
    }

    public void OnDisable()
    {
        _goToNextStateButtonUI.OnClick -= HandleGoToNight;
        _openSpellBookButtonUI.OnClick -= HandleOpenSpellBook;
        foreach (var button in _chooseBuildingButtonsUI)
        {
            button.OnClick -= HandleChooseBuilding;
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

    private void HandleGoToNight()
    {
        GoToNextState?.Invoke();
    }

    private void HandleOpenSpellBook()
    {
        OpenSpellBook?.Invoke();
    }

    private void HandleChooseBuilding(GameObject building)
    {
        ChooseBuilding?.Invoke(building);
    }
}
