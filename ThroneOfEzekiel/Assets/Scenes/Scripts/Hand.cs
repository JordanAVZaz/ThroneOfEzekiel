using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    public Deck deck; // Reference to the Deck object to draw cards from
    [Range(0,200)]
    public float handWidth = 90;
    [Range(-100,100)]
    public float handY = 0;
    [Range(-100,100)]
    public float handZ = 0;
    [Range(0,10)]
    public int handSize;
    [Range(-10,20)]
    public float maxSpace = 14.3f;
    private int previousHandSize;
    private int startHandSize = 5;
    private List<GameObject> hand = new List<GameObject>();
    private Vector3 handAnchor;

    void Start()
    {
        deck = new Deck("JSON/Decks/Radiant_Starter"); // Initialize the deck with the path to your JSON file
        //deck.ProcessCards(); // Process cards to populate the instantiatedCards list in the deck
        handAnchor = CreateHandAnchor();
        DrawStartingHand(startHandSize);
        previousHandSize = startHandSize;
        handSize = startHandSize;
        maxSpace = 14.3f;
    }

    void Update()
    {
        if(handSize > previousHandSize)
        {
            for(int i = 0; i < handSize - previousHandSize; i++)
            {
                DrawCard();
            }
        }
        previousHandSize = handSize; // Update previous hand size for the next frame
        UpdateCardPlacement();
    }

    private void DrawStartingHand(int size)
    {
    
        for (int i = 0; i < size; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        GameObject drawnCard = deck.Draw(); // Draw a card from the deck

        if (drawnCard != null)
        {
            //GameObject cardObject = Instantiate(cardPrefab);
            // Assuming UnitCard has a method like SetProperties to set its properties based on the drawnCard
            //cardObject.GetComponent<UnitCard>().SetProperties(drawnCard); 
            hand.Add(drawnCard);
            UpdateCardPlacement();
        }
        else
        {
            Debug.LogError("No more cards in the deck.");
        }
    }

    void UpdateCardPlacement()
    {
        float cardSpacing = Mathf.Min(handWidth / (hand.Count - 1), maxSpace);

        for (int i = 0; i < hand.Count; i++)
        {
            float x = handAnchor.x + cardSpacing * (i - ((float)hand.Count - 1) / 2);
            hand[i].transform.position = new Vector3(x, handY, handZ);
        }
    }

    private Vector3 CreateHandAnchor()
    {
        float cameraPosition_X = Camera.main.transform.position.x;
        return new Vector3(cameraPosition_X, handY, handZ);
    }
}
