using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameState _currentState = GameState.Start;
    public GameState currentState => _currentState;

    private Turn _currentTurn = Turn.Start;
    public Turn CurrentGameState => _currentTurn;

    public Dictionary<GameState, Action> GameStateEvent;
    public Dictionary<Turn, Action> GameTurnEvent;

    #region Game_StartStateValue
    private void GameStart()
    {

        // 본인 유닛 선택


    }

    #endregion

    #region Game_SettingStateValue
    private void GameSetting()
    {

        // Setting

    }

    #endregion

    #region Game_BattleStateValue
    [Header("GameBattle Info")]
    [SerializeField] private Unit _playerUnit;
    [SerializeField] private Unit _enemyUnit;

    private bool _isBattleEnd = false;

    private void GameBattle()
    {

        // Turn

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

    }

    #endregion

    protected override void Init()
    {

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
