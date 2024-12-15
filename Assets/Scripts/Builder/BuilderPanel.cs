using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuilderPanel: MonoBehaviour
{
    [SerializeField]
    private BuilderPanelRenderer _builderPanelRenderer;

    [SerializeField]
    private BuilderButtonRenderer _builderButtonRenderer;

    [Inject]
    private BuilderModel _builderModel;

    [Inject]
    private GameStateModel _gameStateModel;

    public void Start()
    {
        _builderPanelRenderer.OnSelectBuilding += HandleSelectBuilding;
        _builderButtonRenderer.OnOpenBuilderPanel += HandleOpenBuilderPanel;
    }

    public void Update()
    {
        if (_gameStateModel.State.Value == GameState.SELECT_TOWER && Input.GetKeyDown(KeyCode.Escape))
        {
            HandleHideBuilderPanel();
        }
    }
    
    public void OnDisable()
    {
        _builderPanelRenderer.OnSelectBuilding -= HandleSelectBuilding;
        _builderButtonRenderer.OnOpenBuilderPanel -= HandleOpenBuilderPanel;
    }

    public void ShowBuilderPanel()
    {
        _gameStateModel.State.Value = GameState.SELECT_TOWER;
    }

    public void HideBuilderPanel()
    {
        _gameStateModel.State.Value = GameState.IDLE;
    }

    private void HandleSelectBuilding(TowerModel model)
    {
        if (model == null)
        {
            _builderModel.SelectedBuilding = null;
            return;
        }

        _builderModel.SelectedBuilding = _builderModel.GetBuildingPrefabByModel(model);

        HideBuilderPanel();
        _gameStateModel.State.Value = GameState.PLACE_TOWER;
    }

    private void HandleOpenBuilderPanel()
    {
        _gameStateModel.State.Value = GameState.SELECT_TOWER;
    }

    private void HandleHideBuilderPanel()
    {
        _gameStateModel.State.Value = GameState.IDLE;
    }
}
