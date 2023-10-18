using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// handles cards going to new collections and setting their indexes, it can also shuffle collections of cards.
public class CardManager : MonoBehaviour
{
    private static CardManager _instance;

    public static CardManager Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<CardManager>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void MigrateCard(Card card, List<Card> oldCollection, List<Card> newCollection)
    {
        Card oldCard = card;
        newCollection.Add(card);
        card.SetIndex(newCollection.Count-1);
        oldCollection.RemoveAt(oldCard.indexID);
    }

    public List<Card> ShuffleCollection(List<Card> collection)
    {
        System.Random rng = new System.Random();
        int n = collection.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = collection[k];
            collection[k] = collection[n];
            collection[n] = value;
        }
        return collection;
    }
}
