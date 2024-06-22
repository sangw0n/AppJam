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
    public readonly int HASH_ATTACK = Animator.StringToHash("OnAttack");
    public readonly int HASH_DIE    = Animator.StringToHash("OnDie");
    public readonly int HASH_IDLE =   Animator.StringToHash("OnIdle");

    // Info
    private SpriteRenderer _unitSprite;
    private Animator _animator;
    private UnitData _unitData;
    private HealthPoint _health;
    private AudioSource _hitAudioSource;
    private Transform _effectSpawnPos;

    // Get
    public HealthPoint UnitHP => _health;
    public UnitData UnitData { get { return _unitData; } set { _unitData = value; } }
    public Animator Animator => _animator;
    public Transform EffectSpawnPos { get { return _effectSpawnPos; } set { _effectSpawnPos = value; } }

    [Header("Info")]
    [SerializeField]
    private UnitUI _unitUI;
    [SerializeField]
    private Unit _opponentUnit;
    public Unit OpponentUnit => _opponentUnit;
    [SerializeField]
    public GameObject effectPrefab;

    private List<BattleEventData> _unitEventDatas = new List<BattleEventData>();
    private Dictionary<Turn, List<UnitEvent>> _unitEventDictionary;
    private UnitEventDataObject _unitEventObject;

    public void Init()
    {
        // GetCompo
        _health = transform.Find("HP").GetComponent<HealthPoint>();
        _unitSprite = transform.Find("Visual").GetComponent<SpriteRenderer>();
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _effectSpawnPos = transform.Find("EffectSpawnPos").GetComponent<Transform>();

        _hitAudioSource = transform.Find("HitSound").GetComponent<AudioSource>();
    }

    public void HitDamage(int value)
    {
        Destroy(Instantiate(effectPrefab, EffectSpawnPos.position, Quaternion.identity), 1.0f);
        _hitAudioSource.Play();
        _health.AddValue(-value);
        StartCoroutine(HitDamageCoroutine());
        GameManager.Instance.CameraShake(0.1f);

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

        // eventData
        _unitEventObject = Instantiate(_unitData.EventData, transform);
        _unitEventDatas = _unitEventObject.UnitEventDatas;

        // Set Value
        _unitSprite.sprite = _unitData.UnitSprite;
        _animator.runtimeAnimatorController = _unitData.UnitAnimController;
        _health.SetHealthInfo(_unitData.MaxHealth, _unitData.MaxHealth, _animator, HASH_DIE);
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
            for(int i = 0; i < e.UnitGameEvent.Weight; ++i)
            {
                _unitEventDictionary[e.ActivatedGameState].Add(e.UnitGameEvent);
            }

        });

    }
    public List<UnitEvent> GetTurnEvent(Turn turn) => _unitEventDictionary[turn];

}
