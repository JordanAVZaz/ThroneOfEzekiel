using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class BaseTile : InteractiveObject, ISubscribeToMouseClicks
{
    //Which players summoning zone is it in.
    public int playerX_summoningZone;
    private Card _occupyingCard;
    //position on grid and ID
    public Tuple<int, int> gridID;
    // Cached reference to the Mouse component
    private Mouse _mouse;
    public Card GetOccupyingCard => _occupyingCard;
    public Tile3D tile3D;
    public CardList deck;

    // Start is called before the first frame update
    public void Init(int row, int col)
    {
        deck = new CardList();
        gridID = new Tuple<int, int>(row, col);
        _occupyingCard = null;
        tile3D = new Tile3D(this.gameObject);
        tile3D.FillDefaultColor();
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

    // clicked object found with raycast via subscription
    public void OnMouseClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return; // Ensure this tile was the one clicked

        switch (GameState.Instance.State)
        {
            case GameState.Global_States.HandCardSelected:
                // Place the selected card onto the field
                CardList hand = GlobalPlayerManager.Instance.GetActivePlayer().hand.cardsInHand;
                MigrateToMe(true, GlobalPlayerManager.Instance.GetActivePlayer().hand.cardsInHand);
                Debug.Log("Place the card on the tile");
                break;

            case GameState.Global_States.BoardCardSelected:
                BaseTile incomingTile = GameBoard.Instance.FindTileOfSelectable(_mouse.SelectedCard);
                MigrateToMe(false, incomingTile.deck);
                Debug.Log("Target this tile");
                //if you dont decrement the incoming tile it wont be able to do itself even if it refrences
                //a list since it would need to detect that update inorder to have the latest.
                incomingTile.DecrementOccupyingCard();
                break;

            default:
                Debug.LogError("On Mouse Click Illegal State");
                break;
        }
    }

    private void MigrateToMe(bool fromHand, CardList cardlist)
    {
        if (_occupyingCard == null && _mouse.SelectedCard != null)
        {
            Card cardToMigrate = _mouse.SelectedCard;

            if (!cardlist.Contains(cardToMigrate))
            {
                Debug.LogError($"Attempted to migrate a card not present in the list: {cardToMigrate.name}, I have {cardlist.Count} cards in the source list");
                return;
            }

            if (cardlist.MigrateCardTo(cardToMigrate, deck))
            {
                cardToMigrate.transform.localPosition = this.transform.localPosition;
                if (fromHand)
                {
                    cardToMigrate.gameObject.layer = 7; //board layer
                    cardToMigrate.card3D.SetBoardSize();
                }
                _occupyingCard = cardToMigrate;
                Debug.Log($"Card Placed on {gridID} Tile");
            }
        }
        else
        {
            Debug.Log("Illegal Card Placement Attempt");
        }
    }
    //to be used on deck getting migrated from
    public void DecrementOccupyingCard()
    {
        if (deck.Count < 1)
        {
            _occupyingCard = null;
        }
        else
        {
            _occupyingCard = deck[deck.Count - 1];
        }
    }
    // it's more efficient with the cached mouse component
    public void OnEnable()
    {
        _mouse.OnMouseClick += OnMouseClick;
    }

    public void OnDisable()
    {
        _mouse.OnMouseClick -= OnMouseClick;
    }
}
