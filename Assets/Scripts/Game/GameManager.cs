using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    private CastleModel _playerModel;

    [Inject]
    private GameTimeModel _gameTimeModel;

    [SerializeField]
    private GameController _gameController;

    [SerializeField]
    private SkipDayButtonRenderer _skipDayButtonRenderer;

    [SerializeField]
    private GameTimeRenderer _gameTimeRenderer;

    [SerializeField]
    private NightCountNotificationRenderer _nightCountNotificationRenderer;

    private bool _isNotificationAboutNightWasShowed = false;

    public void Update()
    {
        if (_gameTimeModel.IsNight() && !_isNotificationAboutNightWasShowed)
        {
            _isNotificationAboutNightWasShowed = true;
            _nightCountNotificationRenderer.ShowNotification();
        }

        if (_gameTimeModel.IsDay() && _isNotificationAboutNightWasShowed)
        {
            _isNotificationAboutNightWasShowed = false;
        }

        if (_playerModel.Health.Value < 0)
        {
            HandleEndGame();
        }
    }

    public void OnDisable()
    {
        _skipDayButtonRenderer.OnSkipDay -= HandleSkipDay;
        DOTween.KillAll();
    }
    
    public void Start()
    {
        _skipDayButtonRenderer.OnSkipDay += HandleSkipDay;
    }

    private void HandleSkipDay()
    {
        _gameController.SkipDay();
    }

    private void HandleEndGame()
    {
        SceneManager.LoadScene("EndGame");
    }
}
