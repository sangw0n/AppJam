using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BattleEventData
{

    public GameState ActivatedGameState;
    public UnitEvent UnitGameEvent;

}

public class Unit : MonoBehaviour
{

    // Hash

    // Info
    private SpriteRenderer _unitSprite;
    private UnitData _unitData;
    private HealthPoint _health;

    [Header("Info")]
    [SerializeField]
    private UnitDataSO _unitDataSO;
    [SerializeField]
    private UnitUI _unitUI;

    [SerializeField]
    private List<BattleEventData> _unitEventDatas = new List<BattleEventData>();
    private Dictionary<GameState, List<UnitEvent>> _unitEventDictionary;

    private void Start()
    {

        // GetCompo
        _health = transform.Find("HP").GetComponent<HealthPoint>();
        _unitSprite = transform.Find("Visual").GetComponent<SpriteRenderer>();

        // Get Value
        _unitData = _unitDataSO.MyUnitData;

        // Set Value
        _unitSprite.sprite = _unitData.UnitSprite;
        _health.SetHealthInfo(_unitData.MaxHealth, _unitData.MaxHealth);
        _unitUI.SetName(_unitData.UnitName);

        _unitEventDictionary = new Dictionary<GameState, List<UnitEvent>>()
        {
            { GameState.Start, new List<UnitEvent>() },
            { GameState.PlayerTurn, new List<UnitEvent>() },
            { GameState.Battle, new List<UnitEvent>() },
            { GameState.End, new List<UnitEvent>() },
            { GameState.Setting, new List<UnitEvent>() },
            { GameState.GameEnd, new List<UnitEvent>() },
        };

        // Init
        _unitEventDatas.ForEach(e =>
        {

            e.UnitGameEvent.SetOwner(this);
            _unitEventDictionary[e.ActivatedGameState].Add(e.UnitGameEvent);

        });

    }




}
