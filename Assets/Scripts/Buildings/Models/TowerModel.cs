using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModel : MonoBehaviour
{
    [SerializeField]
    private string towerName;

    [SerializeField]
    private string towerDescription;

    [SerializeField]
    private int cost;

    [SerializeField]
    private int square;

    public string TowerName { get { return towerName; } }

    public string TowerDescription { get { return towerDescription; } }

    public int Cost { get { return cost; } }

    public int Square { get { return square; } }
}
