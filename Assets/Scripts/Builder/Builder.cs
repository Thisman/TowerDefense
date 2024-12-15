using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Zenject;

public class Builder : MonoBehaviour
{
    [SerializeField]
    private TileBase _tileUnderTower;

    [SerializeField]
    private TileBase _tileAfterDestroyTower;

    [SerializeField]
    private GameController _gameController;

    [Inject]
    private BuilderBankModel _builderBankModel;

    [Inject]
    private GameTimeModel _gameTimeModel;

    [Inject]
    private GameStateModel _gameStateModel;

    [Inject]
    private BuilderModel _builderModel;

    [Inject]
    private MapTowerBuilder _mapTowerBuilder;

    [Inject]
    private MapHighlighter _mapHighlighter;

    [Inject]
    private MapModel _mapModel;

    [Inject]
    private MapTerraformer _mapTerraformer;

    private List<Vector3Int> _highlightedArea;

    public void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_gameStateModel.State.Value == GameState.IDLE && Input.GetMouseButtonDown(2))
        {
            HandleDestroyTower();
        }

        if (_gameStateModel.State.Value == GameState.PLACE_TOWER)
        {
            HandleHighlighterPlaceForTower(mouseWorldPos);
        }

        if (_gameStateModel.State.Value == GameState.PLACE_TOWER && Input.GetKey(KeyCode.Escape))
        {
            HandleStopConstruction();
        }

        if (_gameStateModel.State.Value == GameState.PLACE_TOWER && Input.GetMouseButtonDown(0) && _gameTimeModel.IsDay())
        {
            HandlePlaceTower(mouseWorldPos);
        }
    }

    private void HandleStopConstruction()
    {
        if (_highlightedArea != null)
        {
            _mapHighlighter.ResetAreaHighlight(_highlightedArea);
            _highlightedArea = null;
        }

        _builderModel.SelectedBuilding = null;
        _gameStateModel.State.Value = GameState.IDLE;
    }

    private void HandlePlaceTower(Vector3 mousePosition)
    {
        GameObject tower = _builderModel.SelectedBuilding;
        TowerModel towerModel = tower.GetComponent<TowerModel>();
        Vector3Int towerPosition = _mapModel.Map.WorldToCell(mousePosition);

        if (!_builderBankModel.IsEnoughMoney(towerModel.Cost))
        {
            return;
        }

        if (_mapTowerBuilder.TryBuild(towerPosition, _highlightedArea, tower, gameObject.transform))
        {
            _builderBankModel.BySomething(towerModel.Cost);
            _mapTerraformer.FillArea(_mapModel.GetTilesArea(towerPosition, towerModel.Square), _tileUnderTower);
            HandleStopConstruction();
        }
    }

    private void HandleDestroyTower()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePosition);

        if (hit != null && hit.gameObject.CompareTag("Tower"))
        {
            GameObject tower = hit.gameObject;
            Vector3Int? towerPosition = _mapTowerBuilder.GetTowerPosition(tower);
            if (towerPosition != null)
            {
                TowerModel towerModel = tower.GetComponent<TowerModel>();
                // (TODO) Move to tower controller
                Sequence animation = DOTween.Sequence();
                animation
                    .Append(tower.transform.DOScale(1.2f, 0.1f))
                    .Append(tower.transform.DOScale(0, 0.1f))
                    .OnComplete(() => {
                        if (towerModel != null && _mapTowerBuilder.TryDestroy((Vector3Int)towerPosition))
                        {
                            _builderBankModel.AddMoney(towerModel.Cost);
                            _mapTerraformer.FillArea(_mapModel.GetTilesArea((Vector3Int)towerPosition, towerModel.Square), _tileAfterDestroyTower);
                        }
                    });
            }
        }
    }

    private void HandleHighlighterPlaceForTower(Vector3 mousePosition)
    {
        GameObject tower = _builderModel.SelectedBuilding;
        TowerModel towerModel = tower.GetComponent<TowerModel>();

        Vector3Int centerPosition = _mapModel.Map.WorldToCell(mousePosition);
        List<Vector3Int> areaAroundTower = _mapModel.GetTilesArea(centerPosition, towerModel.Square);
        List<Color> areaColors = new();

        foreach (Vector3Int tilePosition in areaAroundTower)
        {
            TileTemplate tileData = _mapModel.GetTileData(tilePosition);
            if (tileData == null)
            {
                continue;
            }

            areaColors.Add(tileData.IsAvailableForBuilding ? Color.green : Color.red);
        }

        if (_highlightedArea != null)
        {
            _mapHighlighter.ResetAreaHighlight(_highlightedArea);
        }

        _mapHighlighter.HighlightTileArea(areaAroundTower, areaColors);
        _highlightedArea = areaAroundTower;
    }
}
