using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BattleEventData
{

    public Turn ActivatedGameState;
    public UnitEvent UnitGameEvent;

}

public class Unit : MonoBehaviour
{

    // Hash
    private readonly int HASH_SHAKE = Shader.PropertyToID("_VibrateFade");
    private readonly int HASH_BLINK = Shader.PropertyToID("_StrongTintFade");

    // Info
    private SpriteRenderer _unitSprite;
    private UnitData _unitData;
    private HealthPoint _health;

    // Get
    public HealthPoint UnitHP => _health;
    public UnitData UnitData => _unitData;

    [Header("Info")]
    [SerializeField]
    private UnitUI _unitUI;
    [SerializeField]
    private Unit _opponentUnit;
    public Unit OpponentUnit => _opponentUnit;

    [SerializeField]
    private List<BattleEventData> _unitEventDatas = new List<BattleEventData>();
    private Dictionary<Turn, List<UnitEvent>> _unitEventDictionary;

    public void Init()
    {
        // GetCompo
        _health = transform.Find("HP").GetComponent<HealthPoint>();
        _unitSprite = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }

    public void HitDamage(int value)
    {
        
        _health.AddValue(-value);
        StartCoroutine(HitDamageCoroutine());

    }

    private WaitForSeconds _delayTime = new WaitForSeconds(0.1f);
    private IEnumerator HitDamageCoroutine()
    {

        _unitSprite.material.SetFloat(HASH_SHAKE, 1f);
        _unitSprite.material.SetFloat(HASH_BLINK, 1f);
        yield return _delayTime;

        _unitSprite.material.SetFloat(HASH_SHAKE, 0f);
        _unitSprite.material.SetFloat(HASH_BLINK, 0f);

    }

    public void SetUnitData(UnitData unitData)
    {

        // Get Value
        _unitData = unitData;

        // Set Value
        _unitSprite.sprite = _unitData.UnitSprite;
        _health.SetHealthInfo(_unitData.MaxHealth, _unitData.MaxHealth);
        _unitUI.SetName(_unitData.UnitName);

        _unitEventDictionary = new Dictionary<Turn, List<UnitEvent>>()
        {
            { Turn.Start, new List<UnitEvent>() },
            { Turn.PlayerTurn, new List<UnitEvent>() },
            { Turn.Battle, new List<UnitEvent>() },
            { Turn.End, new List<UnitEvent>() },
        };

        // Init
        _unitEventDatas.ForEach(e =>
        {

            e.UnitGameEvent.SetOwner(this);
            _unitEventDictionary[e.ActivatedGameState].Add(e.UnitGameEvent);

        });

    }
    public List<UnitEvent> GetTurnEvent(Turn turn) => _unitEventDictionary[turn];

}
