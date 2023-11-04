using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
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
    private int startHandSize = 5;
    public CardList cardsInHand{get;private set;}
    public Card Selected;
    private Vector3 handAnchor;
   
    void Awake()
    {
        cardsInHand = new CardList();
        handAnchor = CreateHandAnchor();
        handSize = startHandSize;
        maxSpace = 14.3f;
    }

    void Update()
    {
        UpdateCardPlacement();
    }
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
}
