using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoSingleton<Money>
{

    private int _currentGold = 0;
    public int CurrentGold => _currentGold;

    // current
    public event Action<int> OnGoldChanged;

    protected override void Init()
    {

        EarnGold(10000);

    }

    public void EarnGold(int value)
    {
        _currentGold += value;
        OnGoldChanged?.Invoke(_currentGold);
    }

    public void SpendGold(int value)
    {
        _currentGold -= value;
        OnGoldChanged?.Invoke(_currentGold);
    }

}
