using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _resultText;
    [SerializeField]
    private TextMeshProUGUI _explainText;

    [SerializeField]
    private TextMeshProUGUI _rewardText;
    [SerializeField]
    private TextMeshProUGUI _currentGoldText;

    private void OnEnable()
    {

        bool isWin = GameManager.Instance.IsPlayerWin;
        if(isWin)
        {
            
            _resultText.text    = "½Â¸®";
            _explainText.text   = "È¹µæ";

            int rewardValue = Mathf.RoundToInt(GameManager.Instance.BettingGold * GameManager.Instance.CurrentBettingPer);
            _rewardText.text = $"{rewardValue} G";

            Money.Instance.EarnGold(rewardValue);

        }
        else
        {

            _resultText.text = "ÆÐ¹è";
            _explainText.text = "ÀÒÀº °ñµå";

            int rewardValue = (GameManager.Instance.BettingGold);
            _rewardText.text = $"{rewardValue} G";

        }

        // ÇöÀç ¼ÒÁö °ñµå Ç¥½Ã
        _currentGoldText.text = Money.Instance.CurrentGold.ToString();


    }

    public void RewardBtnClickEvent()
    {

        gameObject.SetActive(false);
        GameManager.Instance.ChangeGameState(GameState.Setting);

    }

}
