using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
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
    public static GameState Instance { get; private set; }
    private Global_States currentState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Set_Idle();
    }

    public void Set_GameStart()
    {
        currentState = Global_States.GameStart;
        Console.WriteLine("GameStart");
        Debug.Log("GameStart");
    }

    public void Set_TurnTransition()
    {
        currentState = Global_States.TurnTransition;
        Console.WriteLine("TurnTransition");
        Debug.Log("TurnTransition");
    }

    public void Set_Draw()
    {
        currentState = Global_States.Draw;
        Console.WriteLine("Draw");
        Debug.Log("Draw");
    }

    public void Set_Idle()
    {
        currentState = Global_States.Idle;
        Console.WriteLine("Idle");
        Debug.Log("Idle");
    }

    public void Set_HandCardSelected()
    {
        currentState = Global_States.HandCardSelected;
        Console.WriteLine("HandCardSelected");
        Debug.Log("HandCardSelected");
    }

    public void Set_FieldCardSelected()
    {
        currentState = Global_States.FieldCardSelected;
        Console.WriteLine("FieldCardSelected");
        Debug.Log("FieldCardSelected");
    }

    public void Set_GameEnd()
    {
        currentState = Global_States.GameEnd;
        Console.WriteLine("GameEnd");
        Debug.Log("GameEnd");
    }
    public Global_States State
    {
        get { return currentState; }
        set { currentState = value; }
    }

}
