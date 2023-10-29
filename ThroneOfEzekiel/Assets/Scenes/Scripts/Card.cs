using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card3D card3D;
    public int indexID { get; private set; } = -1;

    public string Name {get; protected set; }

    public void Initialize(string link,int index)
    {
        SetIndex(index);
    }

    public void Selection(bool cardSelected)
    {
        if (card3D != null)
        {
            card3D.VisualizeSelection(cardSelected);
        }
    }

    public void SetIndex(int index)
    {
        indexID = index;
    }
}
