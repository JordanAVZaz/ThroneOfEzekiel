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
        GameEnd
    }
    public delegate void GameStateChangedDelegate(Global_States newState);
    public static event GameStateChangedDelegate OnGameStateChanged;
    public Players _player { private set; get; }
    private Global_States _currentState;
    private int turn;
    protected override void Awake()
    {
        base.Awake();
        _currentState = Global_States.GameStart;
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
                turn = 1;
                _player = Players.Player1;
                // Handle game start logic
                Set_TurnTransition();
                break;

            case Global_States.TurnTransition:
                // Handle turn transition logic
                if (_player == Players.Player2 || _player == Players.Player1 && turn == 1)
                {
                    _player = Players.Player1;

                }
                else if (_player == Players.Player1 && turn > 1)
                {
                    _player = Players.Player2;
                    turn++;
                }
                Set_Draw();
                break;

            case Global_States.Draw:
                // Handle draw logic
                if (turn == 1)
                {
                    GlobalPlayerManager.Instance.GetActivePlayer().Draw(5);
                }
                else
                {
                    GlobalPlayerManager.Instance.Draw();
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

    public void Set_GameEnd()
    {
        State = Global_States.GameEnd;
        Console.WriteLine("GameEnd");
        Debug.Log("GameEnd");
    }


}
