using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class BaseTile : MonoBehaviour
{
    //signature
    public delegate void TileOccupationHandler(GameObject card);
    // event
    public event TileOccupationHandler OnCardMove;
    //card in this tile
    private List<Card> _deck;
    //Which players summoning zone is it in.
    public int playerX_summoningZone;
    private Card _occupyingCard;
    private bool _occupied = false;
    private bool _augment = false;
    private bool _isHovered = false;
    private Vector2 _tileLocation;
    //position on grid and ID
    private Tuple<int, int> _gridID;
    // Cached reference to the Mouse component
    private Mouse _mouse;
    //Tile renderer
    private Renderer _tileRenderer;
    //Color fill of object
    private Color _defaultOutlineColor = new Color(65 / 255.0f, 51 / 255.0f, 30 / 255.0f, 1f);
    private Color _defaultTileColor = new Color(163 / 255.0f, 168 / 255.0f, 142 / 255.0f, 1f);
    private Color _selectableOutlineColor = new Color(0f, 146 / 255.0f, 248 / 255.0f, 1f);
    private Color _selectableTileColor = new Color(35 / 255.0f, 94 / 255.0f, 183 / 255.0f, 1f);
    private Color _nonSelectableOutlineColor = new Color(200 / 255.0f, 50 / 255.0f, 16 / 255.0f, 1f);
    private Color _nonSelectableTileColor = new Color(149 / 255.0f, 40 / 255.0f, 16 / 255.0f, 1f);
    public bool ISelectable { get; protected set; }
    public bool IHover { get; protected set; }
    public bool ITarget { get; protected set; }

    // Start is called before the first frame update
    public void Init(int row, int col)
    {
        _deck = new List<Card>();
        _tileLocation = new Vector2(row, col);
        _gridID = new Tuple<int, int>(row, col);
        _occupyingCard = null;
        SetupRenderer();
        Fill_Default_Color();
    }

    private void SetupRenderer()
    {
        if (_tileRenderer == null)
            _tileRenderer = GetComponent<Renderer>();
    }

    void Awake()
    {
        _mouse = FindObjectOfType<Mouse>();
        if (_mouse != null)
        {
            //Debug.Log("Mouse Linked");
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
    void Update() { }

    public void Put_In(Card card)
    {
        //CardManager.Instance.MigrateCard(card,);
    }

    public void Fill_Default_Color()
    {
        if (_tileRenderer != null)
        {
            _tileRenderer.material.SetColor("_OutlineColor", _defaultOutlineColor);
            _tileRenderer.material.SetColor("_BaseColor", _defaultTileColor);
            _isHovered = false;
        }
    }

    public void Fill_Selectable_Color()
    {
        if (_tileRenderer != null && playerX_summoningZone == 1)
        {
            _tileRenderer.material.SetColor("_OutlineColor", _selectableOutlineColor);
            _tileRenderer.material.SetColor("_BaseColor", _selectableTileColor);
            _isHovered = true;
        }
        else
        {
            Fill_NonSelectable_Color();
        }
    }

    private void Fill_NonSelectable_Color()
    {
        if (_tileRenderer != null)
        {
            _tileRenderer.material.SetColor("_OutlineColor", _nonSelectableOutlineColor);
            _tileRenderer.material.SetColor("_BaseColor", _nonSelectableTileColor);
            _isHovered = true;
        }
    }

    public void MigrateCard(Card card)
    {
        switch (GameState.State)
        {
            case GameState.Global_States.HandCardSelected:

                if (_occupyingCard == null)
                {
                    _occupyingCard = card;
                    GlobalPlayerManager.Instance.GetActivePlayer().hand.cardsInHand.MigrateCardTo(_occupyingCard, _deck);
                    //board layer
                    _occupyingCard.gameObject.layer = 1 << 7;
                    _occupyingCard.transform.localPosition = this.transform.localPosition;
                    GameState.Instance.Set_Idle();
                    Debug.Log("Card Placed on Tile");
                    break;
                }
                else
                {
                    Debug.Log("Illegal Card Placement Attempt");
                    break;
                }

            case GameState.Global_States.BoardCardSelected:

                if (_occupyingCard == null)
                {
                    _occupyingCard = card;
                    GlobalPlayerManager.Instance.GetActivePlayer().hand.cardsInHand.MigrateCardTo(_occupyingCard, _deck);
                    _occupyingCard.transform.localPosition = this.transform.localPosition;
                    GameState.Instance.Set_Idle();
                    Debug.Log("Card Placed on new Tile");
                    break;
                }
                else if (_occupyingCard != null)
                {
                    Debug.Log("Attack");
                    break;
                }
                else
                {
                    Debug.Log("Illegal Card Placement Attempt");
                    break;
                }
        }
    }

    public void OnStateChange()
    {
        switch (GameState.State)
        {
            case (GameState.Global_States.Idle):
                ITarget = false;
                ISelectable = true;
                IHover = false;
                break;
            case (GameState.Global_States.HandCardSelected):
                ITarget = true;
                ISelectable = false;
                IHover = true;
                break;
            case (GameState.Global_States.BoardCardSelected):
                ITarget = true;
                ISelectable = false;
                IHover = true;
                break;
            default:
                ITarget = false;
                ISelectable = false;
                IHover = false;
                break;
        }
    }

    public bool IsHovered => _isHovered;
    public bool IsOccupied
    {
        get => _occupied;
        set => _occupied = value;
    }

    public bool IsAugmented
    {
        get => _augment;
        set => _augment = value;
    }

    public Vector2 TileLocation
    {
        get => _tileLocation;
        private set => _tileLocation = value;
    }
}
