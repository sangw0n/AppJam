using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    PlayerTurn,
    Battle,
    End,

    Setting,
    GameEnd,
}

public class GameManager : MonoSingleton<GameManager>
{

    private GameState _currentGameState = GameState.Setting;
    public GameState CurrentGameState => _currentGameState;

    public Dictionary<GameState, Action> GameStateEvent;


    protected override void Init()
    {
        
    }

    // Flow
    




}
