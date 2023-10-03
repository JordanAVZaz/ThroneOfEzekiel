using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    public int player_x_SummoningZone;
    private bool occupied = false;
    private bool augment = false;
    private Vector2 tileLocation;
    private bool isHovered = false;
    private Renderer tileRenderer;
    private Tuple<int, int> gridID;
    private Color defaultOutlineColor = new Color(65 / 255.0f, 51 / 255.0f, 30 / 255.0f, 1f);
    private Color defaultTileColor = new Color(163 / 255.0f, 168 / 255.0f, 142 / 255.0f, 1f);
    private Color selectableOutlineColor = new Color(0f, 146 / 255.0f, 248 / 255.0f, 1f);
    private Color selectableTileColor = new Color(35 / 255.0f, 94 / 255.0f, 183 / 255.0f, 1f);
    private Color nonSelectableOutlineColor = new Color(200 / 255.0f, 50 / 255.0f, 16 / 255.0f, 1f);
    private Color nonSelectableTileColor = new Color(149 / 255.0f, 40 / 255.0f, 16 / 255.0f, 1f);

    // Start is called before the first frame update
    public void Init(int row, int col)
    {
        tileLocation = new Vector2(row, col);
        gridID = new Tuple<int, int>(row, col);
        SetupRenderer();
        Fill_Default_Color();
    }

    private void SetupRenderer()
    {
        if (tileRenderer == null)
            tileRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        SetupRenderer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fill_Default_Color()
    {
        if (tileRenderer != null)
        {
            tileRenderer.material.SetColor("_OutlineColor", defaultOutlineColor);
            tileRenderer.material.SetColor("_BaseColor", defaultTileColor);
            isHovered = false;
        }
    }

    public void Fill_Selectable_Color()
    {
        if (tileRenderer != null && player_x_SummoningZone == 1)
        {
            tileRenderer.material.SetColor("_OutlineColor", selectableOutlineColor);
            tileRenderer.material.SetColor("_BaseColor", selectableTileColor);
            isHovered = true;
        }
        else
        {
            Fill_NonSelectable_Color();
        }
    }
    private void Fill_NonSelectable_Color()
    {
        if (tileRenderer != null)
        {
            tileRenderer.material.SetColor("_OutlineColor", nonSelectableOutlineColor);
            tileRenderer.material.SetColor("_BaseColor", nonSelectableTileColor);
            isHovered = true;
        }
    }
    public bool IsHovered
    {
        get { return isHovered; }
    }

    public bool IsOccupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    public bool IsAugmented
    {
        get { return augment; }
        set { augment = value; }
    }

    public Vector2 TileLocation
    {
        get { return tileLocation; }
        private set { tileLocation = value; }
    }


}
