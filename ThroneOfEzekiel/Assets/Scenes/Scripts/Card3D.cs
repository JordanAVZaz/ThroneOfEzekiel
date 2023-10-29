using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3D : MonoBehaviour
{
    public float scaleFactor = 1.5f;
    private Vector3 originalScale;
    private Renderer cardRenderer;
    private Color defaultOutlineColor = new Color(48 / 255.0f, 7 / 255.0f, 51 / 255.0f, 255 / 255.0f);
    private Color selectionOutlineColor = new Color(168 / 255.0f, 28 / 255.0f, 178 / 255.0f, 255 / 255.0f);

    private void Start()
    {
        originalScale = transform.localScale;
        cardRenderer = GetComponent<Renderer>();

        if (cardRenderer != null)
        {
            cardRenderer.material.SetColor("_OutlineColor", defaultOutlineColor);
        }
        else
        {
            Debug.LogError("Renderer not found on " + gameObject.name);
        }
    }

    public void VisualizeSelection(bool cardSelected)
    {
        if (cardSelected)
        {
            GameState.Instance.Set_HandCardSelected();
            ScaleCard(scaleFactor);
            SetOutlineColor(selectionOutlineColor);
        }
        else
        {
            GameState.Instance.Set_Idle();
            ScaleCard(1.0f);
            SetOutlineColor(defaultOutlineColor);
        }
    }
    public void ScaleCard()
    {
        transform.localScale = originalScale * scaleFactor;
    }
    public void ScaleCard(float scaleMultiplier)
    {
        transform.localScale = originalScale * scaleMultiplier;
    }

    public void ScaleReset()
    {
        transform.localScale = originalScale;
    }

    public void ReLocate(float x,float y,float z)
    {
        transform.localPosition = new Vector3(x,y,z);
    }
    public void ReLocate(Vector3 loaction)
    {
        transform.localPosition = loaction;
    }

    private void SetOutlineColor(Color color)
    {
        if (cardRenderer != null)
        {
            cardRenderer.material.SetColor("_OutlineColor", color);
        }
    }
}
