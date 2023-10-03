using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberBox_Handler : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public GameObject background;
    private int number_value;

    public void Initialize(int val)
    {
        Current_Value = val;
    }
    public void Initialize(int val, Color Background, Color Stroke)
    {
        Current_Value = val;
        Set_Colour(Background, Stroke);
    }

    public int Current_Value
    {
        get { return number_value; }
        set
        {
            if (textMeshPro != null)
            {
                number_value = value;
                textMeshPro.text = number_value.ToString();
            }
            else
            {
                Debug.LogError("TextMeshPro is not assigned.");
            }
        }
    }

    public void Set_Colour(Color Background, Color Stroke)
    {
        if (background != null)
        {
            Renderer renderer = background.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.SetColor("_OutlineColor", Stroke);
                renderer.material.SetColor("_BaseColor", Background);
            }
            else
            {
                Debug.LogError("No Renderer component found on the background GameObject.");
            }
        }
        else
        {
            Debug.LogError("Background GameObject is not assigned.");
        }
    }
}
