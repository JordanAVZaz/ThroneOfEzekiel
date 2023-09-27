using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public float scaleFactor = 1.5f;
    private bool isSelected = false;
    private Vector3 originalScale;
    private Renderer cardRenderer;
    private Color defaultOutlineColor = new Color(48 / 255.0f, 7 / 255.0f, 51 / 255.0f, 255 / 255.0f);
    private Color selectionOutlineColor = new Color(168 / 255.0f, 28 / 255.0f, 178 / 255.0f, 255 / 255.0f);

    void Start()
    {
        originalScale = transform.localScale;
        cardRenderer = GetComponent<Renderer>();
        cardRenderer.material.SetColor("_OutlineColor", defaultOutlineColor);
    }

    public void Selection(bool cardSelected)
    {
        IsSelected = cardSelected;
        if (cardSelected)
        {
            GameState.Instance.Set_HandCardSelected();
            Scale_Card();
            cardRenderer.material.SetColor("_OutlineColor", selectionOutlineColor);
        }
        else
        {
            GameState.Instance.Set_Idle();
            Scale_Card_Reset();
            cardRenderer.material.SetColor("_OutlineColor", defaultOutlineColor);
        }
    }

    public void Scale_Card()
    {
        transform.localScale = originalScale * scaleFactor;
    }

    public void Scale_Card_Reset()
    {
        transform.localScale = originalScale;
    }

    private bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }
}
