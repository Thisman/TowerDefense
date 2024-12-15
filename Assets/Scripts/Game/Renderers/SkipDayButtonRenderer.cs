using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkipDayButtonRenderer : MonoBehaviour
{
    public event Action OnSkipDay;

    [SerializeField]
    private Button _skipDayButtonUI;

    [Inject]
    private GameTimeModel _gameTimeModel;

    public void Start()
    {
        _skipDayButtonUI.onClick.AddListener(() => OnSkipDay.Invoke());
    }

    public void Update()
    {
        if (_gameTimeModel == null)
        {
            return;
        }

        if (_skipDayButtonUI.gameObject.activeSelf != _gameTimeModel.IsDay())
        {
            _skipDayButtonUI.gameObject.SetActive(_gameTimeModel.IsDay());
        }
    }
}
