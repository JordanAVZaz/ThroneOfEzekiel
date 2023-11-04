using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Card3D
{
    private float _scaleFactor = 1f;
    public GameObject cardObject;
    private Vector3 _originalScale;//cache original scale
    private Vector3 _scale;//used scale
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
        _scale = _originalScale;
        if (_cardRenderer != null)
        {
            _cardRenderer.material.SetColor("_OutlineColor", _defaultOutlineColor);
        }
        else
        {
            Debug.LogError("Renderer not found on " + _transform.gameObject.name);
        }
    }

    public void SetBoardSize()
    {
       _scale = _originalScale * .5F;  
        _scaleFactor = 1f;
    }

    public void SetHandSize()
    {
        _scale = _originalScale;  
        _scaleFactor = 1f;
    }

    public void VisualizeSelection()
    {
        ScaleCard(1.5f);
        SetOutlineColor(_selectionOutlineColor);
    }

    public void VisualizeHover()
    {
        ScaleCard(1.5f);
        SetOutlineColor(_defaultOutlineColor);
    }

    public void VisualizeDefault()
    {
        ScaleCard(_scaleFactor);
        SetOutlineColor(_defaultOutlineColor);
    }

    public void ScaleCard()
    {
        _transform.localScale = _scale * _scaleFactor;
    }

    public void ScaleCard(float scaleMultiplier)
    {
        _transform.localScale = _scale * scaleMultiplier;
    }

    public void ScaleReset()
    {
        _transform.localScale = _scale;
    }

    private void SetOutlineColor(Color color)
    {
        if (_cardRenderer != null)
        {
            _cardRenderer.material.SetColor("_OutlineColor", color);
        }
    }
}

