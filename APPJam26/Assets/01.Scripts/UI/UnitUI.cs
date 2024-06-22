using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{

    private TextMeshProUGUI _name;

    private TextMeshProUGUI _healthText;
    private Slider _healthSlider;

    [SerializeField]
    private HealthPoint _unitHP;

    private void Awake()
    {

        // GetCompo
        _name = transform.Find("UnitName").GetComponent<TextMeshProUGUI>();
        _healthText = transform.Find("UnitHealthText").GetComponent<TextMeshProUGUI>();
        _healthSlider = transform.Find("UnitHealthSlider").GetComponent<Slider>();

        _unitHP.OnHealthChanged += UpdateUIEvent;

    }

    public void SetName(string name)
    {
        _name.text = name;
    }
    private void UpdateUIEvent(int currentHP, int maxHP)
    {

        if (_healthText != null)
            _healthText.text = $"{maxHP} / {currentHP}";

        if (_healthSlider != null)
            _healthSlider.value = currentHP / (float)maxHP;

    }
}
