using Cinemachine;
using DG.Tweening;
using Febucci.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Turn
{
    Start,
    PlayerTurn,
    Battle,
    End,
}

public enum GameState
{

    Start,
    Setting,
    Battle,
    GameEnd,
    Shop

}

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Game Info")]
    public List<UnitDataSO> UnitDatas;

    private GameState _currentState = GameState.Start;
    public GameState currentState => _currentState;

    private Turn _currentTurn = Turn.Start;
    public Turn CurrentGameState => _currentTurn;

    public Dictionary<GameState, Action> GameStateEvent;
    public Dictionary<Turn, Action> GameTurnEvent;

    [Header("Camera")]
    public CinemachineVirtualCamera VCam;
    private CinemachineBasicMultiChannelPerlin VCamPerlin;

    [Header("UI")]
    public GameObject SelectUnitPanel;
    public GameObject ShopPanel;
    public GameObject SelectEnemyPanel;
    public GameObject BettingPanel;
    public GameObject RewardPanel;
    public GameObject CommentartPanel;

    public AudioSource UIClickSound;
    public AudioSource OpenPanelSound;

    [Header("Betting Info")]
    private float _currentBettingPer;
    private int _bettingGold;
    private Money _money;

    [Header("Commentary Info ")]
    [SerializeField]
    private TextMeshProUGUI textCommentary;
    [SerializeField]
    private TypewriterByCharacter typewriterByCharacter;


    public float CurrentBettingPer => _currentBettingPer;
    public int BettingGold => _bettingGold;

    #region Game_StartStateValue
    private void GameStart()
    {

        // ���� ���� ����
        SelectUnitPanel.SetActive(true);
        OutBounceAnimationPanel(SelectUnitPanel);

    }

    #endregion

    #region Game_SettingStateValue
    private void GameSetting()
    {

        _playerUnit.UnitHP.AddValue(999);

        // Setting
        SelectEnemyPanel.SetActive(true);
        OutBounceAnimationPanel(SelectEnemyPanel);

    }

    public void SetBettingPer(float bettingPer)
    {
        _currentBettingPer = bettingPer;
    }

    public void SetBettingGold(int bettingGold)
    {
        _bettingGold = bettingGold;
    }

    #endregion

    #region Game_BattleStateValue
    [Header("GameBattle Info")]
    [SerializeField] private Unit _playerUnit;
    [SerializeField] private Unit _enemyUnit;


    public float PlayerUnitPower => (_playerUnit.UnitData.Strength * 2 + _playerUnit.UnitData.MaxHealth);

    private bool _isBattleEnd = false;

    private void GameBattle()
    {

        // Turn
        _isBattleEnd = false;
        _currentTurn = Turn.Start;
        _currentTurnIndex = 0;
        StartCoroutine(BattleTurnLoopCoroutine());

    }

    int _currentTurnIndex = 0;
    private Turn[] _turnFlow = { Turn.Start, Turn.PlayerTurn, Turn.Battle, Turn.End };

    int _turnLoopCount = 0;
    IEnumerator BattleTurnLoopCoroutine()
    {
        _turnLoopCount = 0;

        while (!_isBattleEnd)
        {

            if (++_turnLoopCount >= 100)
                break;


            List<UnitEvent> playerUnitEvents;
            if (_currentTurn == Turn.PlayerTurn)
                playerUnitEvents = _playerUnit.GetTurnEvent(Turn.Battle);
            else
                playerUnitEvents = _playerUnit.GetTurnEvent(_currentTurn);

            List<UnitEvent> enemyUnitEvents = _enemyUnit.GetTurnEvent(_currentTurn);

            if (playerUnitEvents.Count > 0 && _currentTurn != Turn.Battle)
            {
                UnitEvent playerUnitEvent = playerUnitEvents[Random.Range(0, playerUnitEvents.Count)];

                playerUnitEvent.Init();
                playerUnitEvent.InvokeEvent();
                Debug.Log("Player Turn");
                yield return new WaitUntil(() => playerUnitEvent.IsEnd());

            }

            if (_isBattleEnd)
                break;

            if (enemyUnitEvents.Count > 0)
            {
                UnitEvent enemyUnitEvent = enemyUnitEvents[Random.Range(0, enemyUnitEvents.Count)];

                enemyUnitEvent.Init();
                enemyUnitEvent.InvokeEvent();
                yield return new WaitUntil(() => enemyUnitEvent.IsEnd());

            }

            _currentTurnIndex = (_currentTurnIndex + 1) % 4;
            _currentTurn = _turnFlow[_currentTurnIndex];

        }

        ChangeGameState(GameState.GameEnd);
    }

    private void BattleEndEvent()
    {

        _isBattleEnd = true;

    }

    #endregion

    #region Game_GameEndStateValue
    bool _isPlayerWin = false;
    public bool IsPlayerWin => _isPlayerWin;

    private void GameEnd()
    {

        // ���� �׾��°� üũ
        _isPlayerWin = _playerUnit.UnitHP.CurrentHealth > 0;

        RewardPanel.SetActive(true);
        OutBounceAnimationPanel(RewardPanel);

    }

    #endregion

    #region Game_GameShop

    private void GameShop()
    {

        ShopPanel.SetActive(true);
        OutBounceAnimationPanel(ShopPanel);

    }



    #endregion

    private void Start()
    {
        if (instance == null)
        {

            instance = this;
            Init();

        }
    }

    public void OutBounceAnimationPanel(GameObject uiObject)
    {
        OpenPanelSound.Play();

        Transform tweeningObject = uiObject.transform.GetChild(0);
        tweeningObject.localScale = new Vector3(1, 0, 1);
        tweeningObject.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
    }

    protected override void Init()
    {

        _playerUnit.Init();
        _enemyUnit.Init();
        _money = Money.Instance;

        VCamPerlin = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _playerUnit.UnitHP.OnDie += BattleEndEvent;
        _enemyUnit.UnitHP.OnDie += BattleEndEvent;

        GameStateEvent = new Dictionary<GameState, Action>()
        {
            { GameState.Start,      GameStart },
            { GameState.Setting,    GameSetting },
            { GameState.Battle,     GameBattle },
            { GameState.GameEnd,    GameEnd },
            { GameState.Shop,       GameShop },
        };

        ChangeGameState(GameState.Start);

    }

    public void ChangeGameState(GameState state)
    {

        _currentState = state;
        GameStateEvent[state]?.Invoke();

    }

    public void CameraShake(float time)
    {
        StartCoroutine(CameraShakeCo(time));
    }

    IEnumerator CameraShakeCo(float time)
    {
        VCamPerlin.m_AmplitudeGain = 4f;
        VCamPerlin.m_FrequencyGain = 4f;
        yield return new WaitForSeconds(time);
        VCamPerlin.m_AmplitudeGain = 0f;
        VCamPerlin.m_FrequencyGain = 0f;
    }

    public void PlayUIClickSound()
    {
        UIClickSound.Play();
    }


   
}
