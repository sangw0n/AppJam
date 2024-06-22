using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoSingleton<Money>
{

    private int _currentGold = 0;
    public int CurrentGold => _currentGold;

    [SerializeField]
    private TextMeshProUGUI _goldTextUI;

    // current
    public event Action<int> OnGoldChanged;

    protected override void Init()
    {

        OnGoldChanged += HandleUpdateGoldText;
        EarnGold(10000);

    }

    private void HandleUpdateGoldText(int gold)
    {

        _goldTextUI.text = $"¼ÒÁö±Ý : {gold} G";

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
