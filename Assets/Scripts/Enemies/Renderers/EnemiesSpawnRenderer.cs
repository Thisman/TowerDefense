using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class EnemiesSpawnRenderer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _waveLevelTextUI;

    [Inject]
    private EnemiesSpawnModel _enemiesSpawnModel;

    public void Start()
    {
        _enemiesSpawnModel.OnCurrentWaveChanges += HandleWaveChanged;
    }

    private void HandleWaveChanged(EnemiesWaveModel newWave)
    {
        _waveLevelTextUI.text = newWave?.WaveName;
    }
}
