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
    private float _bettingGold;
    private Money _money;

    #region Game_StartStateValue
    private void GameStart()
    {

        // 본인 유닛 선택
        SelectUnitPanel.SetActive(true);

    }

    #endregion

    #region Game_SettingStateValue
    private void GameSetting()
    {

        // Setting
        SelectEnemyPanel.SetActive(true);

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
        _currentTurn = Turn.Start;
        StartCoroutine(BattleTurnLoopCoroutine());

    }

    int _currentTurnIndex = 0;
    private Turn[] _turnFlow = { Turn.Start, Turn.PlayerTurn, Turn.Battle, Turn.End };

    IEnumerator BattleTurnLoopCoroutine()
    {

        while(!_isBattleEnd)
        {

            List<UnitEvent> playerUnitEvents = _playerUnit.GetTurnEvent(_currentTurn);
            List<UnitEvent> enemyUnitEvents = _enemyUnit.GetTurnEvent(_currentTurn);

            //
            if (playerUnitEvents.Count > 0)
            {
                UnitEvent playerUnitEvent = playerUnitEvents[Random.Range(0, playerUnitEvents.Count)];

                playerUnitEvent.Init();
                playerUnitEvent.InvokeEvent();
                yield return new WaitUntil(playerUnitEvent.IsEnd);

            }

            if (_isBattleEnd)
                yield break;

            if (enemyUnitEvents.Count > 0)
            {
                UnitEvent enemyUnitEvent = enemyUnitEvents[Random.Range(0, enemyUnitEvents.Count)];

                enemyUnitEvent.Init();
                enemyUnitEvent.InvokeEvent();
                yield return new WaitUntil(enemyUnitEvent.IsEnd);

            }

            _currentTurnIndex = (_currentTurnIndex + 1) % 4;
            _currentTurn = _turnFlow[_currentTurnIndex];

        }
        

    }

    private void BattleEndEvent()
    {

        _isBattleEnd = true;
        ChangeGameState(GameState.GameEnd);

    }

    #endregion

    #region Game_GameEndStateValue
    private void GameEnd()
    {
        ChangeGameState(GameState.Start);
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
