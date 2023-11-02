using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card3D card3D;
    public int indexID = 0;
    public string Name { get; protected set; }
    public bool Selected { get; set; }
    public bool ISelectable { get; protected set; }
    public bool IHover { get; protected set; }

    public virtual void Initialize(string link, int index)
    {
        indexID = index;
        card3D = new Card3D(gameObject);
        Selected = false;
    }

    public void Select()
    {
        switch (card3D.cardObject.layer)
        {
            //deck layer
            case (1):
                UnityEngine.Debug.LogError("Illegal to select deck card");
                break;
            //hand layer
            case (6):
                card3D.ScaleFactor = 1f;
                GameState.Instance.Set_HandCardSelected();
                card3D.VisualizeSelection();
                break;
            //board layer
            case (7):
                card3D.ScaleFactor = .5f;
                GameState.Instance.Set_BoardCardSelected();
                card3D.VisualizeSelection();
                break;
            default:
                UnityEngine.Debug.LogError("Card is outside layer scope");
                UnityEngine.Debug.LogError("layer:" + card3D.cardObject.layer);
                break;
        }
    }

    public void Reset()
    {
        card3D.ScaleReset();
    }

    public void EventHandle(){
        
    }
    //controls 
    public void OnStateChange()
    {
        switch (GameState.State)
        {
            case (GameState.Global_States.Idle):
                ISelectable = true;
                IHover = true;
                break;
            case (GameState.Global_States.HandCardSelected):
                ISelectable = true;
                IHover = false;
                break;
            case (GameState.Global_States.BoardCardSelected):
                ISelectable = true;
                IHover = false;
                break;
            default:
                ISelectable = false;
                IHover = true;
                break;
        }
    }

    public void OnEnable()
    {
        GameState.onStateChange += OnStateChange;
    }

    public void OnDisable()
    {
        GameState.onStateChange += OnStateChange;
    }
}
