using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBankModel
{
    public Action<int> OnMoneyChange;

    private int _money = 9999;
    private int _maxMoney = 9999999;

    public BuilderBankModel()
    {
        if (_money > _maxMoney)
        {
            _money = _maxMoney;
        }
    }

    public int Money { get { return _money; } }

    public bool BySomething(int cost)
    {
        if (cost > _money)
        {
            return false;
        }

        _money -= cost;
        OnMoneyChange.Invoke(_money);
        return true;
    }

    public bool IsEnoughMoney(int cost)
    {
        return _money >= cost;
    }

    public void AddMoney(int changes)
    {
        if (_money < 0)
        {
            Debug.LogError("Can't add negative value");
            return;
        }

        _money += changes;
        if (_money > _maxMoney)
        {
            _money = _maxMoney;
        }

        OnMoneyChange.Invoke(_money);
    }

}
