using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour, IMigrateCards
{
    [Range(0, 200)]
    public float handWidth = 90;
    [Range(-100, 100)]
    public float handY = 0;
    [Range(-100, 100)]
    public float handZ = 0;
    [Range(0, 10)]
    public int handSize;
    [Range(-10, 20)]
    public float maxSpace = 14.3f;
    private int previousHandSize;
    private int startHandSize = 5;
    public CardList cardsInHand = new CardList();
    private Vector3 handAnchor;

    void Start()
    {
        handAnchor = CreateHandAnchor();
        //DrawStartingHand(startHandSize);
        //previousHandSize = startHandSize;
        handSize = startHandSize;
        maxSpace = 14.3f;
    }

    void Update()
    {   /*
        //editor card draw for testing
        if (handSize > previousHandSize)
        {
            for (int i = 0; i < handSize - previousHandSize; i++)
            {
                GlobalLogicalDeckManager.Instance.Draw();//will draw correct from manager due to encapsulation
            }
        }
        previousHandSize = handSize; // Update previous hand size for the next frame
        */
        UpdateCardPlacement();
    }
/*
    private void DrawStartingHand(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GlobalLogicalDeckManager.Instance.Draw();//will draw from correct manager due to encapsulation
        }
    }
*/
    public void UpdateCardPlacement()
    {
        float cardSpacing = Mathf.Min(handWidth / (cardsInHand.Count - 1), maxSpace);

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            float x = handAnchor.x + cardSpacing * (i - ((float)cardsInHand.Count - 1) / 2);
            cardsInHand[i].transform.position = new Vector3(x, handY, handZ);
        }
    }

    private Vector3 CreateHandAnchor()
    {
        float cameraPosition_X = Camera.main.transform.position.x;
        return new Vector3(cameraPosition_X, handY, handZ);
    }
    public CardList get => cardsInHand;
    public void MigrateCardTo(Card card, List<Card> newCollection)
    {
        cardsInHand.MigrateCardTo(card, newCollection);
    }
}
