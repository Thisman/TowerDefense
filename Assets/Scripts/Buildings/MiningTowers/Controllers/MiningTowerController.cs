using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class MiningTowerController : MonoBehaviour
{
    [SerializeField]
    private MiningTowerModel miningTowerModel;

    [Inject]
    private BuilderBankModel builderBankModel;

    private float timer;

    public void Start()
    {
        timer = miningTowerModel.MiningDelaySec;
    }

    public void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            builderBankModel.AddMoney(miningTowerModel.MiningMoneyCount);
            timer = miningTowerModel.MiningDelaySec;
        }
    }
}
