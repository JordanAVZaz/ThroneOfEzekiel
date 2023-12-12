using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile3D 
{
    private Renderer _tileRenderer;

    public GameObject gameObject;
    public Color DefaultOutlineColor { get; set; }
    public Color DefaultTileColor { get; set; }
    public Color SelectableOutlineColor { get; set; }
    public Color SelectableTileColor { get; set; }
    public Color NonSelectableOutlineColor { get; set; }
    public Color NonSelectableTileColor { get; set; }

    public Tile3D(GameObject tileObject)
    {
        //setup renderer
        gameObject = tileObject;
        _tileRenderer = tileObject.GetComponent<Renderer>();
        if (_tileRenderer == null)
        {
            Debug.LogError("Renderer not found on the tile");
        }
        // Initialize colors here or provide them through another method such as a constructor or initializer
        DefaultOutlineColor = new Color(65 / 255.0f, 51 / 255.0f, 30 / 255.0f, 1f);
        DefaultTileColor = new Color(163 / 255.0f, 168 / 255.0f, 142 / 255.0f, 1f);
        SelectableOutlineColor = new Color(0f, 146 / 255.0f, 248 / 255.0f, 1f);
        SelectableTileColor = new Color(35 / 255.0f, 94 / 255.0f, 183 / 255.0f, 1f);
        NonSelectableOutlineColor = new Color(200 / 255.0f, 50 / 255.0f, 16 / 255.0f, 1f);
        NonSelectableTileColor = new Color(149 / 255.0f, 40 / 255.0f, 16 / 255.0f, 1f);
    }

    public void FillDefaultColor()
    {
        if (_tileRenderer != null)
        {
            _tileRenderer.material.SetColor("_OutlineColor", DefaultOutlineColor);
            _tileRenderer.material.SetColor("_BaseColor", DefaultTileColor);
        }
    }

    public void FillSelectableColor()
    {
        if (_tileRenderer != null)
        {
            _tileRenderer.material.SetColor("_OutlineColor", SelectableOutlineColor);
            _tileRenderer.material.SetColor("_BaseColor", SelectableTileColor);
        }
    }

    public void FillNonSelectableColor()
    {
        if (_tileRenderer != null)
        {
            _tileRenderer.material.SetColor("_OutlineColor", NonSelectableOutlineColor);
            _tileRenderer.material.SetColor("_BaseColor", NonSelectableTileColor);
        }
    }
}

