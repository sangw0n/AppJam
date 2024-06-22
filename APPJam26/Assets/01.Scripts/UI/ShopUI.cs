using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI goldText;
    [SerializeField]
    TextMeshProUGUI _statText;

    [SerializeField]
    private Unit _playerUnit;

    [SerializeField]
    private Transform _failTrm;
    [SerializeField]
    private TextMeshProUGUI _failText;

    [SerializeField]
    private List<string> _failTexts;

    private void Awake()
    {
        Money.Instance.OnGoldChanged += HandleUpdateGold;
    }

    private void OnEnable()
    {
        HandleUpdateGold(Money.Instance.CurrentGold);
    }

    public void SmallUpgradeHealth()
    {
        RandomUpgradeHealthStat(0.9f, 1, 50);
    }

    public void BigUpgradeHealth()
    {
        RandomUpgradeHealthStat(0.5f, 80, 100);
    }

    public void SmallUpgradeStrength()
    {
        RandomUpgradeStrengthStat(0.9f, 1, 10);
    }

    public void BigUpgradeStrength()
    {
        RandomUpgradeStrengthStat(0.5f, 15, 30);
    }

    public void NextBtnClickEvent()
    {

        gameObject.SetActive(false);
        GameManager.Instance.ChangeGameState(GameState.Setting);

    }

    private void RandomUpgradeHealthStat(float per, int minValue, int maxValue)
    {
        if (Money.Instance.CurrentGold < 1000)
            return;

        Money.Instance.SpendGold(1000);

        float randomValue = Random.Range(0f, 1f);
        if(randomValue <= per)
        {

            UnitData data = _playerUnit.UnitData;
            data.MaxHealth = data.MaxHealth + Random.Range(minValue, maxValue + 1);

            _playerUnit.UnitData = data;

            UpdateStatData();

        }
        else
        {

            FailText();

        }

    }
    private void RandomUpgradeStrengthStat(float per, int minValue, int maxValue)
    {
        if (Money.Instance.CurrentGold < 1000)
            return;

        Money.Instance.SpendGold(1000);

        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= per)
        {

            UnitData data = _playerUnit.UnitData;
            data.Strength = data.Strength + Random.Range(minValue, maxValue + 1);

            _playerUnit.UnitData = data;

            UpdateStatData();

        }
        else
        {

            FailText();

        }

    }

    private void FailText()
    {
        _failTrm.gameObject.SetActive(true);

        _failText.text = _failTexts[Random.Range(0, _failTexts.Count)];
        _failText.color = Color.white;
        _failTrm.localScale = Vector3.zero;


        Sequence seq = DOTween.Sequence();
        seq.Append(_failTrm.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic));
        seq.Append(_failTrm.DOScale(Vector3.zero, 0.2f));
        seq.OnComplete(() =>
        {
            _failTrm.gameObject.SetActive(false);
        });

    }

    private void UpdateStatData()
    {
        UnitData unitData = _playerUnit.UnitData;

        _statText.text = $"공격력 - {unitData.Strength}\n체력 - {unitData.MaxHealth}";

        _playerUnit.UnitHP.SetHealthInfo(unitData.MaxHealth, unitData.MaxHealth);

    }

    private void HandleUpdateGold(int gold)
    {
        goldText.text = $"소지금 - {gold}";
    }

}
