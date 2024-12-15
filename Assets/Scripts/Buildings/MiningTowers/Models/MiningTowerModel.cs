using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTowerModel : TowerModel
{
    [SerializeField]
    private int miningMoneyCount;

    [SerializeField]
    private int miningDelaySec;

    public int MiningMoneyCount { get { return miningMoneyCount; } }

    public int MiningDelaySec { get { return miningDelaySec; } }
}
 