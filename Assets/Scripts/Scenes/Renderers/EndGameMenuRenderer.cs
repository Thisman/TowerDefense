using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenuRenderer : MonoBehaviour
{
    public Action OnGoToMainMenu;

    [SerializeField]
    private Button _mainMenuButtonUI;

    [SerializeField]
    private TextMeshProUGUI _endGameTextUI;

    public void Create()
    {
        _mainMenuButtonUI.onClick.AddListener(() => OnGoToMainMenu.Invoke());
    }
}
