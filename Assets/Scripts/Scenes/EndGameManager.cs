using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private EndGameMenuRenderer _endGameMenuRenderer;

    public void Start()
    {
        _endGameMenuRenderer.Create();
        _endGameMenuRenderer.OnGoToMainMenu += HandleGoToMainMenu;
    }

    private void HandleGoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
