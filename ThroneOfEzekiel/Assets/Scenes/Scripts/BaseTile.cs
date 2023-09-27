using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private bool occupied = false;
    private bool augment = false;
    private Vector2 tileLocation;
    //flag for race conditions on hover
    private bool isHovered = false;
    private Renderer tileRenderer;
    private Color defaultOutlineColor = new Color(65 / 255.0f, 51 / 255.0f, 30 / 255.0f, 1f);
    private Color defaultTileColor = new Color(163 / 255.0f, 168 / 255.0f, 142 / 255.0f, 1f);
    private Color selectableOutlineColor = new Color(0f, 146 / 255.0f, 248 / 255.0f, 1f);
    private Color selectableTileColor = new Color(35 / 255.0f, 94 / 255.0f, 183 / 255.0f, 1f);


    // Start is called before the first frame update
    public void Init(Vector2 location)
    {
        tileLocation = location;
        tileRenderer = GetComponent<Renderer>();
        Fill_Default_Color();
    }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Fill_Default_Color()
    {
        tileRenderer.material.SetColor("_OutlineColor", defaultOutlineColor);
        tileRenderer.material.SetColor("_BaseColor", defaultTileColor);
        isHovered = false;
    }
    public void Fill_Selectable_Color()
    {
        tileRenderer.material.SetColor("_OutlineColor", selectableOutlineColor);
        tileRenderer.material.SetColor("_BaseColor", selectableTileColor);
        isHovered = true;
    }
    public bool IsHovered { get; }
    public bool IsOccupied { get; set; }
    public bool IsAugmented { get; set; }
    public Vector2 TileLocation { get; private set; }

}
