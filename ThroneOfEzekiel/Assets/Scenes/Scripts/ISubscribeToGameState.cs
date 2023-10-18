using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscribeToGameState
{
    void HandleGameStateChange(GameState.Global_States newState);

    void OnEnable();
    /*{
        GameState.OnGameStateChanged += HandleGameStateChange;
    }*/

    void OnDisable();
    /*{
        GameState.OnGameStateChanged -= HandleGameStateChange;
    }*/
}

