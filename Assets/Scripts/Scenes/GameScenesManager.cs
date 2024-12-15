using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesManager : MonoBehaviour
{
    [SerializeField]
    private SceneMenuRenderer _sceneMenuRenderer;

    public void Start()
    {
        _sceneMenuRenderer.Create();
        _sceneMenuRenderer.OnStartGame += HandleStartGame;
        _sceneMenuRenderer.OnExitGame += HandleExitGame;
    }

    public void OnDisable()
    {
        _sceneMenuRenderer.OnStartGame -= HandleStartGame;
        _sceneMenuRenderer.OnExitGame -= HandleExitGame;
    }

    private void HandleStartGame()
    {
        SceneManager.LoadScene("Demo");
    }

    private void HandleExitGame()
    {
        Debug.Log("Игра завершена.");
        Application.Quit();
    }
}
