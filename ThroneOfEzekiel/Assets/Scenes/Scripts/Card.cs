using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Card : InteractiveObject
{
    public Card3D card3D;
    public int indexID = 0;
    public string Name { get; protected set; }
    public Player Owner {get;set;}

    public virtual void Initialize(string link, int index)
    {
        indexID = index;
        card3D = new Card3D(gameObject);
    }

    public void Selection(bool cardSelected)
    {
        if (card3D != null && cardSelected)
        {
            switch (card3D.cardObject.layer)
            {
                //deck layer
                case (1):
                    UnityEngine.Debug.LogError("Illegal to select deck card");
                    break;
                //hand layer
                case (6):
                    GameState.Instance.Set_HandCardSelected();
                    card3D.VisualizeSelection();
                    break;
                //board layer
                case (7):
                    GameState.Instance.Set_BoardCardSelected();
                    VisualiseAttackPattern();
                    card3D.VisualizeSelection();
                    break;
                default:
                    UnityEngine.Debug.LogError("Card is outside layer scope");
                    UnityEngine.Debug.LogError("layer:" + card3D.cardObject.layer);
                    break;
            }
        }
        else
        {
            GameState.Instance.Set_Idle();
            card3D.VisualizeDefault();
        }
    }

    private void VisualiseAttackPattern()
    {
        if (this is UnitCard unitcard)
        {
            BoardVisualiser.Instance.AttackPattern(unitcard.unitClass, GameBoard.Instance.FindTileOfSelectable(this));
        }
        else
        {
            Debug.LogError("Can't show non unit attack pattern");
        }
    }


}
