using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SelectEnemyUI : MonoBehaviour
{
    [SerializeField]
    private Unit _enemyUnit;

    [SerializeField]
    List<UnitCardUI> _cards;
    List<float> _cardBettingPer;

    [SerializeField]
    List<TextMeshProUGUI> _bettingPer;
    

    private void Awake()
    {

        for (int i = 0; i < _cards.Count; i++)
        {

            _cards[i].ClickEvent += OnClickEvent;

        }

    }

    public void OnEnable()
    {

        _cardBettingPer = new List<float>();
        List<UnitDataSO> unitDatas = GameManager.Instance.UnitDatas.ToList<UnitDataSO>();

        for (int i = 0; i < _cards.Count; i++)
        {

            int randomIndex = Random.Range(i, unitDatas.Count);

            UnitData unitData = unitDatas[randomIndex].MyUnitData;
            _cards[i].SetData(unitData);

            // Betting Per
            float unitPower = (unitData.Strength * 2 + unitData.MaxHealth);
            float bettingPer = 1.5f + Mathf.Clamp((Mathf.Round((unitPower - GameManager.Instance.PlayerUnitPower) * 0.1f) * 0.1f), 0.1f, 5f);

            _cardBettingPer.Add(bettingPer);
            _bettingPer[i].text = $"베팅율 : {bettingPer}x";

            unitDatas[randomIndex] = unitDatas[i];

        }


    }

    public void OnClickEvent(UnitData data)
    {
        // Betting Panel
        GameManager.Instance.BettingPanel.SetActive(true);
        GameManager.Instance.OutBounceAnimationPanel(GameManager.Instance.BettingPanel);
        _enemyUnit.SetUnitData(data);

        // 선택된 카드 인덱스
        for(int i = 0; i < _cards.Count; ++i)
        {

            if(data.UnitName == _cards[i].CardUnitData.UnitName)
            {

                GameManager.Instance.SetBettingPer(_cardBettingPer[i]);
                break;

            }

        }

        gameObject.SetActive(false);

    }



}
