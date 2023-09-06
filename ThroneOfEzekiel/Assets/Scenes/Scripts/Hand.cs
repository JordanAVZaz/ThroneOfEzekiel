using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    public int startHandSize = 7;
    public float screenHandWidth = 90;
    public float leftCardX = -20;
    public float handY = 0;
    public float handZ = 0;
    public float spacing = 1.5f;

    private List<GameObject> hand;
    private int currentHandSize;

    void Start()
    {
        hand = new List<GameObject>();
        currentHandSize = startHandSize;
       // InitializeHand();

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 midpoint = cameraPosition + Camera.main.transform.forward * 5f; // Adjust the multiplier to set the distance from the camera at which the cards should appear.

        // Calculate the starting position based on the number of cards and spacing
        Vector3 startPos = midpoint - new Vector3(spacing * (currentHandSize / 2), 0, 0);

        for (int i = 0; i < currentHandSize; i++)
        {
            Vector3 cardPosition = startPos + new Vector3(spacing * i, 0, 0);
            GameObject card = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
            hand.Add(card);
        }
    }

    void Update()
    {
        int queuedCards = currentHandSize;
        foreach(GameObject card in hand){
            card.transform.position = new Vector3(screenHandWidth / currentHandSize*queuedCards, handY, handZ);
            queuedCards--;
        }
    }

    void InitializeHand()
    {
        int queuedCards = currentHandSize;
        for (int i = 0; i < currentHandSize; i++)
        {
            Draw(new Vector3
            (screenHandWidth / currentHandSize*queuedCards, handY, handZ), Quaternion.identity);
            queuedCards--;
        }
    }

    void Draw(Vector3 cardTransform, Quaternion cardRotate)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardTransform, cardRotate);
        hand.Add(cardObject);
    }

    void CardPlacement()
    {
        // Logic to arrange cards in desired format
    }
}
