using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    [Range(0,1000)]
    public float handWidth = 90;
    [Range(-100,100)]
    public float handY = 0;
    [Range(-100,100)]
    public float handZ = 0;
    [Range(0,10)]
    public int handSize;
    //spacing limiter, 14.3 is the favourite
    [Range(0,100)]
    public float maxSpace = 14.3f; 

    private int startHandSize = 7;
    private List<GameObject> hand = new List<GameObject>();
    private Vector3 handAnchor;

    void Start()
    {
        InitializeHand();
        handSize = startHandSize;
    }

    void Update()
    {
        ResizeHand();
        UpdateCardPlacement();
    }

    private void InitializeHand()
    {
        handSize = startHandSize;
        handAnchor = CreateHandAnchor();

        for (int i = 0; i < startHandSize; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab);
            hand.Add(cardObject);
        }

        UpdateCardPlacement();
    }

    void UpdateCardPlacement()
    {  
        //card spacing formula
        //handWidth = x screen size for hand
        //maxSpace = limiter for how far cards can be apart
        float cardSpacing = Mathf.Min(handWidth / (hand.Count - 1), maxSpace);

        //formula fans out cards from centre
        for (int i = 0; i < hand.Count; i++)
        {
            float x = handAnchor.x + cardSpacing * (i - ((float)hand.Count - 1) / 2);
            hand[i].transform.position = new Vector3(x, handY, handZ);
        }
    }
    //test funtion
    private void ResizeHand(){
        while (handSize < hand.Count)
        {
            Destroy(hand[hand.Count - 1]);
            hand.RemoveAt(hand.Count - 1);
        }

        while (handSize > hand.Count)
        {
            GameObject cardObject = Instantiate(cardPrefab);
            hand.Add(cardObject);
        }
    }

    private Vector3 CreateHandAnchor()
    {
        float cameraPosition_X = Camera.main.transform.position.x;
        return new Vector3(cameraPosition_X, handY, handZ);
    }
}
