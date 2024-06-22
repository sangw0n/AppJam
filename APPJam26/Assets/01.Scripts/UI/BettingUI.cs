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

    }

    public void PressBettingBtn()
    {

        GameManager.Instance.SetBettingGold(int.Parse(_bettingField.text));
        GameManager.Instance.ChangeGameState(GameState.Battle);
        gameObject.SetActive(false);

    }

}
