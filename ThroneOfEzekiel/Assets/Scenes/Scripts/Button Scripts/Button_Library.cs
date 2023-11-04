using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Library : MonoBehaviour
{
    public void Draw_Button()
    {
        GlobalPlayerManager.Instance.GetActivePlayer().Draw();
    }
    public void TurnPass_Button()
    {
        GameState.Instance.Set_EndTurn();
    }
}
