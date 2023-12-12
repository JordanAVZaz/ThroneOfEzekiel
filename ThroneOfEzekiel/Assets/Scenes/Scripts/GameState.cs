using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//state pattern
public class GameState : Singleton<GameState>
{
    public enum Players
    {
        Player1,
        Player2,
        NoPlayer
    }
    public enum Global_States
    {
        GameStart,
        TurnTransition,
        Draw,
        Idle,
        HandCardSelected,
        BoardCardSelected,
        EndTurn,
        GameEnd
    }
    public delegate void GameStateChangedDelegate(Global_States newState);
    public static event GameStateChangedDelegate OnGameStateChanged;
    public Players _playerTurn { private set; get; }
    private Global_States _currentState;
    private int _turn;
    private GlobalPlayerManager _pm;
    protected override void Awake()
    {
        base.Awake();
        _currentState = Global_States.GameStart;
        _pm = GlobalPlayerManager.Instance;
        _playerTurn = Players.NoPlayer;
    }
    private void Update()
    {
        HandleCurrentState();
    }

    private void HandleCurrentState()
    {
        switch (_currentState)
        {
            case Global_States.GameStart:
                _turn = 1;
                // Handle game start logic
                Set_TurnTransition();
                break;

            case Global_States.TurnTransition:
                if (_playerTurn != Players.Player1 || _turn == 1 && _playerTurn == Players.NoPlayer) // If it's an odd turn number, it's Player1's turn
                {
                    _playerTurn = Players.Player1;
                    Debug.Log($"Player 1 turn {_turn}");
                }
                else if (_playerTurn != Players.Player2)// If it's an even turn number, it's Player2's turn
                {
                    _playerTurn = Players.Player2;
                    Debug.Log($"Player 2 turn {_turn}");
                }
                _pm.SetActiveHand();
                Set_Draw();
                break;

            case Global_States.Draw:
                // Handle draw logic
                if (_turn == 1)
                {
                    _pm.GetActivePlayer().Draw(5);
                }
                else
                {
                    _pm.Draw();
                }
                Set_Idle();
                break;

            case Global_States.Idle:
                // Handle idle state logic
                break;

            case Global_States.HandCardSelected:
                //Tile handles it own click via sub
                break;

            case Global_States.BoardCardSelected:
                // Handle when a card from the field is selected
                break;

            case Global_States.EndTurn:
                if (_playerTurn == Players.Player2)
                {
                    _turn++;
                }
                Set_TurnTransition();
                break;

            case Global_States.GameEnd:
                // Handle game end logic
                break;

            default:
                Debug.LogError("Unknown game state: " + _currentState);
                break;
        }
    }
    public Global_States State
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            OnGameStateChanged?.Invoke(_currentState);
        }
    }

    public void Set_GameStart()
    {
        State = Global_States.GameStart;
        Console.WriteLine("GameStart");
        Debug.Log("GameStart");
    }

    public void Set_TurnTransition()
    {
        State = Global_States.TurnTransition;
        Console.WriteLine("TurnTransition");
        Debug.Log("TurnTransition");
    }

    public void Set_Draw()
    {
        State = Global_States.Draw;
        Console.WriteLine("Draw");
        Debug.Log("Draw");
    }

    public void Set_Idle()
    {
        State = Global_States.Idle;
        Console.WriteLine("Idle");
        Debug.Log("Idle");
    }

    public void Set_HandCardSelected()
    {
        State = Global_States.HandCardSelected;
        Console.WriteLine("HandCardSelected");
        Debug.Log("HandCardSelected");
    }

    public void Set_BoardCardSelected()
    {
        State = Global_States.BoardCardSelected;
        Console.WriteLine("FieldCardSelected");
        Debug.Log("FieldCardSelected");
    }

    public void Set_EndTurn()
    {
        State = Global_States.EndTurn;
        Console.WriteLine("EndTurn");
        Debug.Log("EndTurn");
    }

    public void Set_GameEnd()
    {
        State = Global_States.GameEnd;
        Console.WriteLine("GameEnd");
        Debug.Log("GameEnd");
    }
}
