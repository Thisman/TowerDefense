using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMenuRenderer : MonoBehaviour
{
    public event Action OnStartGame;

    public event Action OnExitGame;

    [SerializeField]
    private Button _startGameButtonUI;

    [SerializeField]
    private Button _exitGameButtonUI;

    public void Create()
    {
        _startGameButtonUI.onClick.AddListener(() => OnStartGame.Invoke());
        _exitGameButtonUI.onClick.AddListener(() => OnExitGame.Invoke());
    }
}
