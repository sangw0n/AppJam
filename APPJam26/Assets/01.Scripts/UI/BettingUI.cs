using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettingUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _currentGoldText;
    [SerializeField] TMP_InputField _bettingField;

    private void OnEnable()
    {

        _currentGoldText.text = $"¼ÒÁö±Ý : {Money.Instance.CurrentGold} G";
        ClampValue();

    }

    public void PressBettingBtn()
    {
        int value = int.Parse(_bettingField.text);

        Money.Instance.SpendGold(value);
        GameManager.Instance.SetBettingGold(value);
        GameManager.Instance.ChangeGameState(GameState.Battle);
        gameObject.SetActive(false);

    }
    public void ClampValue()
    {
        if (string.IsNullOrEmpty(_bettingField.text)
            || _bettingField.text == "-")
        {
            _bettingField.text = "100";
        }

        try
        {
            int value = int.Parse(_bettingField.text);
            value = Mathf.Clamp(value, 100, Money.Instance.CurrentGold);

            _bettingField.text = value.ToString();
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }

    }

}
