using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CardList : List<Card>
{
    Dictionary<string, int> cachedCoppies = new Dictionary<string, int>();
    public CardList() : base() { }

    public new void Add(Card card)
    {
        base.Add(card);
        card.SetIndex(this.Count - 1);
    }

    public new bool Remove(Card card)
    {
        int id = card.indexID;

        if (id >= 0 && id < this.Count)
        {
            base.RemoveAt(id); // Removes the card based on its indexID.
            UpdateIndices(id); // Assuming this updates indices starting from the given index.
            this.DecrementCache(card);
            return true;
        }

        return false; // Return false if the card couldn't be removed.
    }


    private void UpdateIndices(int start = 0)
    {
        for (int i = start; i < this.Count; i++)
        {
            this[i].SetIndex(i);
        }
    }

    public void MigrateCardTo(Card card, List<Card> newCollection)
    {
        Card oldCard = card;
        newCollection.Add(card);
        card.SetIndex(newCollection.Count - 1);
        this.RemoveAt(oldCard.indexID);
        //old list should have its cards update their indexes
        this.UpdateIndices(oldCard.indexID);
        this.DecrementCache(card);
    }
    //shuffles a collection
    public void Shuffle()
    {
        System.Random rng = new System.Random();
        //fisher-yates shuffeling algorithm
        int n = this.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = this[k];
            this[k] = this[n];
            this[n] = value;
        }
    }

    public int HowManyCoppes(Card card)
    {
        //check for cached coppies
        var cachedcard = FindCachedCopy(card);
        if (cachedcard != null)
        {
            return cachedcard.Value.Value;
        }

        //creates cached coppy and returns a value
        int coppies = 0;
        foreach (Card c in this)
        {
            if (card.Name == c.Name)
            {
                coppies++;
            }
        }
        cachedCoppies.Add(card.Name, coppies);
        return coppies;
    }

    private bool DecrementCache(Card card)
    {
        var cachedcard = FindCachedCopy(card);
        if (cachedcard.HasValue)
        {
            if (cachedcard.Value.Value < 1)
            {
                UnityEngine.Debug.LogError("Removed Illigal Card copy");
                return false;
            }
            else
            {
                int newValue = cachedcard.Value.Value - 1;
                cachedCoppies[cachedcard.Value.Key] = cachedcard.Value.Value - 1;
            }
        }
        return true;
    }

    private KeyValuePair<string, int>? FindCachedCopy(Card card)
    {
        if (cachedCoppies.TryGetValue(card.Name, out int count))
        {
            return new KeyValuePair<string, int>(card.Name, count);
        }

        UnityEngine.Debug.LogWarning("Card not found in cache: " + card.Name);
        return null;
    }
}



