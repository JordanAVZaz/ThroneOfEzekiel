using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// handles cards going to new collections and setting their indexes, it can also shuffle collections of cards.
public class CardManager : Singleton<CardManager>
{
    //singleton patttern
    private static CardManager _instance;

    // migrate cards to new deck
    public void MigrateCard(Card card, List<Card> oldCollection, List<Card> newCollection)
    {
        Card oldCard = card;
        newCollection.Add(card);
        card.indexID = newCollection.Count - 1;
        oldCollection.RemoveAt(oldCard.indexID);
        //old list should have its cards update their indexes
        if (oldCollection.Count > 0)
        {
            for (int i = oldCard.indexID; i < oldCollection.Count; i++)
            {
                oldCollection[i].GetComponent<Card>().indexID = i;
            }
        }

    }
    //shuffles a collection
    public List<Card> ShuffleCollection(List<Card> collection)
    {
        System.Random rng = new System.Random();
        //fisher-yates shuffeling algorithm
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
