using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player
{
    public CardList deck { get; private set; }

    private Deck _deckMeta;

    public Hand hand { get; private set; }

    private GameObject _hand3D;

    public CardList boardCards = new CardList();

    public CardList grave = new CardList();

    public GameState.Players player;

    public Player(GameState.Players _player, string link)
    {
        player = _player;
        _deckMeta = new Deck(link);
        deck = _deckMeta.deck;

        // Load the prefab
        GameObject handPrefab = Resources.Load<GameObject>("Prefabs/PlayerHand");
        if (handPrefab == null)
        {
            UnityEngine.Debug.LogError("Unable to load PlayerHand prefab.");
            return; // Exit if the prefab wasn't loaded
        }

        // Instantiate the prefab
        GameObject instantiatedHand = UnityEngine.Object.Instantiate(handPrefab);

        // Get the Hand component from the instantiated object
        hand = instantiatedHand.GetComponent<Hand>();
        if (hand == null)
        {
            UnityEngine.Debug.LogError("Instantiated object does not have a Hand component.");
        }
    }
    //generic draw from deck function
    public void Draw()
    {
        if (deck.Count < 1)
        {
            Debug.Log("No more cards to draw.");
            return;
        }

        Card cardToDraw = deck[0];
        cardToDraw.gameObject.SetActive(true);
        cardToDraw.gameObject.layer = 6;

        if (deck.MigrateCardTo(cardToDraw, hand.cardsInHand))
        {
            hand.UpdateCardPlacement();
        }
        else
        {
            // Handle the error if the card couldn't be migrated.
            Debug.LogError("Failed to draw a card from the deck.");
        }
    }
    public void Draw(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Draw();
        }
        hand.UpdateCardPlacement();

    }





}
