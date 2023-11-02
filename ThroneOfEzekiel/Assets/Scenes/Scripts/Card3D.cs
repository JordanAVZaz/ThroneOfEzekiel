using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3D
{
    public float ScaleFactor { get; set; } = 1f;
    public GameObject cardObject;
    private Vector3 _originalScale;
    private Renderer _cardRenderer;
    private Transform _transform;

    private Color _defaultOutlineColor = new Color(48 / 255.0f, 7 / 255.0f, 51 / 255.0f, 255 / 255.0f);
    private Color _selectionOutlineColor = new Color(168 / 255.0f, 28 / 255.0f, 178 / 255.0f, 255 / 255.0f);

    public Card3D(GameObject rootObject)
    {
        cardObject = rootObject;
        _transform = rootObject.transform;
        _cardRenderer = rootObject.GetComponent<Renderer>();
        Initialize();
    }

    private void Initialize()
    {
        _originalScale = _transform.localScale;
        if (_cardRenderer != null)
        {
            _cardRenderer.material.SetColor("_OutlineColor", _defaultOutlineColor);
        }
        else
        {
            Debug.LogError("Renderer not found on " + _transform.gameObject.name);
        }
    }

    public void VisualizeSelection()
    {
        ScaleCard(1.5f);
        SetOutlineColor(_selectionOutlineColor);
    }

    public void VisualizeReset()
    {
        ScaleReset();
        SetOutlineColor(_defaultOutlineColor);
    }

    public void VisualizeHover()
    {
        _transform.localScale = _originalScale * 1.5f;
    }

    public void ScaleCard()
    {
        _transform.localScale = _originalScale * ScaleFactor;
    }

    public void ScaleCard(float scaleMultiplier)
    {
        _transform.localScale = _originalScale * scaleMultiplier;
    }

    public void ScaleReset()
    {
        _transform.localScale = _originalScale;
    }


    private void SetOutlineColor(Color color)
    {
        if (_cardRenderer != null)
        {
            _cardRenderer.material.SetColor("_OutlineColor", color);
        }
    }
}

