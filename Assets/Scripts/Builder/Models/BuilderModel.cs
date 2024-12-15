using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BuilderModel
{
    private GameObject _selectedBuilding;
    private List<GameObject> _buildingPrefabs = new();
    private List<TowerModel> _buildingModels = new();
    private Dictionary<TowerModel, GameObject> _buildingPrefabsDictionary = new();

    public BuilderModel()
    {
        _buildingPrefabs = Resources.LoadAll<GameObject>("Prefabs/Buildings").ToList();
        foreach (var prefab in _buildingPrefabs)
        {
            TowerModel buildingModel = prefab.GetComponent<TowerModel>();
            _buildingModels.Add(buildingModel);
            _buildingPrefabsDictionary.Add(buildingModel, prefab);
        }
    }

    public List<TowerModel> BuildingModels { get { return _buildingModels; } }

    public GameObject SelectedBuilding
    {
        get { return _selectedBuilding; }
        set {
            _selectedBuilding = value;
        }
    }

    public GameObject GetBuildingPrefabByModel(TowerModel model)
    {
        if(!_buildingPrefabsDictionary.ContainsKey(model))
        {
            Debug.LogError("Can't find model " + model.TowerName + " in dictionary");
            return null;
        }

        return _buildingPrefabsDictionary[model];
    }
}
