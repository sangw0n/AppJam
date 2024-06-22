using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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
    GameEnd

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

    [Header("UI")]
    public GameObject SelectUnitPanel;
    public GameObject SelectEnemyPanel;
    public GameObject BettingPanel;
    public GameObject RewardPanel;

    [Header("Betting Info")]
    private float _currentBettingPer;
    private int _bettingGold;
    private Money _money;

    public float CurrentBettingPer => _currentBettingPer;
    public int BettingGold => _bettingGold;

    #region Game_StartStateValue
    private void GameStart()
    {

        // 본인 유닛 선택
        SelectUnitPanel.SetActive(true);
        OutBounceAnimationPanel(SelectUnitPanel);

    }

    #endregion

    #region Game_SettingStateValue
    private void GameSetting()
    {

        _playerUnit.UnitHP.AddValue(9999);

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

        // 누가 죽었는가 체크
        _isPlayerWin = _playerUnit.UnitHP.CurrentHealth > 0;

        RewardPanel.SetActive(true);
        OutBounceAnimationPanel(RewardPanel);

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
        Transform tweeningObject = uiObject.transform.GetChild(0);
        tweeningObject.localScale = new Vector3(1, 0, 1);
        tweeningObject.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
    }

    protected override void Init()
    {

        _playerUnit.Init();
        _enemyUnit.Init();
        _money = Money.Instance;

        _playerUnit.UnitHP.OnDie += BattleEndEvent;
        _enemyUnit.UnitHP.OnDie += BattleEndEvent;

        GameStateEvent = new Dictionary<GameState, Action>()
        {
            { GameState.Start,      GameStart },
            { GameState.Setting,    GameSetting },
            { GameState.Battle,     GameBattle },
            { GameState.GameEnd,    GameEnd },
        };

        ChangeGameState(GameState.Start);

    }

    public void ChangeGameState(GameState state)
    {

        _currentState = state;
        GameStateEvent[state]?.Invoke();

    }



}
