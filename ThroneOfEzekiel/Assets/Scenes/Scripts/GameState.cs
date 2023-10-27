using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//state pattern
public class GameState : Singleton<GameState>
{
    public enum Global_States
    {
        GameStart,
        TurnTransition,
        Draw,
        Idle,
        HandCardSelected,
        FieldCardSelected,
        GameEnd
    }
    public delegate void GameStateChangedDelegate(Global_States newState);
    public static event GameStateChangedDelegate OnGameStateChanged;
    private Global_States currentState;

    private void Update()
    {
        HandleCurrentState();
    }

    private void HandleCurrentState()
    {
        switch (currentState)
        {
            case Global_States.GameStart:
                // Handle game start logic
                Set_TurnTransition();
                break;

            case Global_States.TurnTransition:
                // Handle turn transition logic
                Set_Draw();
                break;

            case Global_States.Draw:
                // Handle draw logic
                Set_Idle();
                break;

            case Global_States.Idle:
                // Handle idle state logic
                break;

            case Global_States.HandCardSelected:
                // Handle when a card from the hand is selected
                break;

            case Global_States.FieldCardSelected:
                // Handle when a card from the field is selected
                break;

            case Global_States.GameEnd:
                // Handle game end logic
                break;

            default:
                Debug.LogError("Unknown game state: " + currentState);
                break;
        }
    }
      public Global_States State
    {
        get { return currentState; }
        set
        {
            currentState = value;
            OnGameStateChanged?.Invoke(currentState);
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

    public void Set_FieldCardSelected()
    {
        State = Global_States.FieldCardSelected;
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
