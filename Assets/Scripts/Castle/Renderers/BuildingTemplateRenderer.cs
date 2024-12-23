using Game.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTemplateRenderer : MonoBehaviour
{
    public Action<BuildingTemplate> OnClicked;

    private BuildingTemplate _buildingTemplate;

    public void Render(BuildingTemplate template)
    {
        _buildingTemplate = template;

        Image buildingIconUI = transform.Find("BuildingIcon").GetComponent<Image>();
        TextMeshProUGUI buildingNameTextUI = transform.Find("BuildingNameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI buildingCostTextUI = transform.Find("BuildingCostText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI buildingDescriptionTextUI = transform.Find("BuildingDescriptionText").GetComponent<TextMeshProUGUI>();

        buildingIconUI.sprite = template.Icon;
        buildingNameTextUI.text = template.Name;
        buildingCostTextUI.text = template.Cost.ToString();
        buildingDescriptionTextUI.text = template.Description;

        Button cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(() => OnClicked?.Invoke(template));
    }
}
