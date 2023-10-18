using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class BaseTile : MonoBehaviour, ISubscribeToMouseClicks
{
    public int player_x_SummoningZone;
    private GameObject current_card;
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

    private Mouse mouse; // Cached reference to the Mouse component

    // Start is called before the first frame update
    public void Init(int row, int col)
    {
        tileLocation = new Vector2(row, col);
        gridID = new Tuple<int, int>(row, col);
        current_card = null;
        SetupRenderer();
        Fill_Default_Color();
    }

    private void SetupRenderer()
    {
        if (tileRenderer == null)
            tileRenderer = GetComponent<Renderer>();
    }

    void Awake()
    {
        mouse = FindObjectOfType<Mouse>();
        if (mouse != null)
        {
            Debug.Log("Mouse Linked");
        }
        else
        {
            Debug.LogError("Mouse not found");
        }

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

    // clicked object found with raycast via subscription
    public void OnMouseClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return; // Ensure this tile was the one clicked

        switch (GameState.Instance.State)
        {
            case GameState.Global_States.HandCardSelected:
                // Place the selected card onto the field
                if (current_card == null && mouse.SelectedObject.GetComponent<Card>() != null)
                {
                    current_card = mouse.SelectedObject;
                    Vector3 newPosition = new Vector3(tileLocation.x, 1, tileLocation.y);
                    current_card.GetComponent<Transform>().SetPositionAndRotation(newPosition, Quaternion.identity);
                    current_card.GetComponent<Transform>().localScale *= 0.5f;
                    mouse.SelectedObject = null;
                    GameState.Instance.Set_Idle();
                    Debug.Log("Card Placed on Tile");
                }
                else
                {
                    Debug.Log("Illegal Card Placement Attempt");
                    break;
                }
                Debug.Log("Place the card on the tile");
                break;

            case GameState.Global_States.FieldCardSelected:
                // Defer action to field card but make this tile the target
                Debug.Log("Target this tile");
                break;

            default:
                Debug.LogError("On Mouse Click Illegal State");
                break;
        }
    }

    // it's more efficient with the cached mouse component
    public void OnEnable()
    {
        mouse.OnMouseClick += OnMouseClick;
    }

    public void OnDisable()
    {
        mouse.OnMouseClick -= OnMouseClick;
    }

    public bool IsHovered => isHovered;
    public bool IsOccupied
    {
        get => occupied;
        set => occupied = value;
    }

    public bool IsAugmented
    {
        get => augment;
        set => augment = value;
    }

    public Vector2 TileLocation
    {
        get => tileLocation;
        private set => tileLocation = value;
    }
}
