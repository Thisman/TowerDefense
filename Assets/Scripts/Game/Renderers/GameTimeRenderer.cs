using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameTimeRenderer : MonoBehaviour
{
    [SerializeField]
    private float _alphaRange = .8f;

    [SerializeField]
    private float _period = 86400f; // Seconds in day

    [SerializeField]
    private SpriteRenderer _skyBox;

    [SerializeField]
    private Sprite _dayIconSprite;

    [SerializeField]
    private Sprite _nightIconSprite;

    [SerializeField]
    private Image _dayStateIconUI;

    [SerializeField]
    private TextMeshProUGUI _dayTimeTextUI;

    [SerializeField]
    private TextMeshProUGUI _daysCountTextUI;

    [Inject]
    private GameTimeModel _gameTimeModel;

    public void FixedUpdate()
    {
        if (_gameTimeModel == null)
        {
            return;
        }

        GameTimeData timeAndDay = _gameTimeModel.GetTimeData();

        if (_gameTimeModel.IsDay())
        {
            _dayStateIconUI.sprite = _dayIconSprite;
        }

        if(_gameTimeModel.IsNight())
        {
            _dayStateIconUI.sprite = _nightIconSprite;
        }

        _dayTimeTextUI.text = AddLeadingZero(timeAndDay.Hours) + " : " + AddLeadingZero(timeAndDay.Minutes);
        _daysCountTextUI.text = "Days: " + AddLeadingZero(timeAndDay.Days);
    }

    public void Update()
    {
        if (_gameTimeModel == null)
        {
            return;
        }

        GameTimeData timeAndDayData = _gameTimeModel.GetTimeData();
        UpdateSkyboxSprite(_gameTimeModel.GetSecondsSinceStartDay(timeAndDayData));
    }

    private string AddLeadingZero(int number)
    {
        if (number < 10)
        {
            return "0" + number;
        }
        else
        {
            return number.ToString();
        }
    }

    private void UpdateSkyboxSprite(float seconds)
    {

        // Compute the raw cosine value
        float rawCosine = Mathf.Cos((2 * Mathf.PI / _period) * seconds);

        // Scale the cosine value from [-1, 1] to [0, maxY]
        float scaledValue = (rawCosine + 1) / 2 * _alphaRange;

        // Apply the new alpha value to the sprite
        Color color = _skyBox.color;
        color.a = scaledValue;
        _skyBox.color = color;
    }
}
