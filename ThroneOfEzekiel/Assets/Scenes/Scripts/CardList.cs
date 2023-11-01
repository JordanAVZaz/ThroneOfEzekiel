using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CardList : List<Card>
{
    Dictionary<string, int> cachedCopies = new Dictionary<string, int>();
    public CardList() : base() { }

    public new void Add(Card card)
    {
        base.Add(card);
        card.indexID = this.Count - 1;

        // Update cache
        if (cachedCopies.ContainsKey(card.Name))
        {
            cachedCopies[card.Name]++;
        }
        else
        {
            cachedCopies[card.Name] = 1;
        }
    }

    public bool RemoveCard(Card card)
    {
        int id = card.indexID;

        if (id >= 0 && id < this.Count)
        {
            base.RemoveAt(id); // Removes the card based on its indexID.
            UpdateIndices(id); // Assuming this updates indices starting from the given index.
            DecrementCache(card);
            return true;
        }

        return false; // Return false if the card couldn't be removed.
    }
    //updates this deck Indices, should be used after crad removal
    private void UpdateIndices(int start = 0)
    {
        for (int i = start; i < this.Count; i++)
        {
            this[i].indexID = i;
        }
    }

    public void MigrateCardTo(Card card, List<Card> newCollection)
    {
        if(this.Count < 1){
            return;
        }
        Card oldCard = card;
        newCollection.Add(card);
        UnityEngine.Debug.Log(oldCard.name + " remove at :" + oldCard.indexID);
        this.RemoveAt(oldCard.indexID);
        //old list should have its cards update their indexes
        this.UpdateIndices(oldCard.indexID);
        this.DecrementCache(card);
    }
    //shuffles a collection
    public void Shuffle()
    {
        System.Random rng = new System.Random();
        int n = this.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = this[k];

            this[k] = this[n];
            this[n] = value;

            // Keepinh indexID consistent with the position, you can do the following:
            this[k].indexID = k;
            this[n].indexID = n;
        }
    }

    public int HowManyCopies(Card card)
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
        cachedCopies.Add(card.Name, coppies);
        return coppies;
    }

    private bool DecrementCache(Card card)
    {
        if (cachedCopies.TryGetValue(card.Name, out int count))
        {
            if (count <= 1)
            {
                cachedCopies.Remove(card.Name); // Remove from cache if count drops to zero
            }
            else
            {
                cachedCopies[card.Name]--;
            }
            return true;
        }
        UnityEngine.Debug.LogError($"Attempted to decrement a non-cached card: {card.Name}");
        return false;
    }

    private KeyValuePair<string, int>? FindCachedCopy(Card card)
    {
        if (cachedCopies.TryGetValue(card.Name, out int count))
        {
            return new KeyValuePair<string, int>(card.Name, count);
        }
        UnityEngine.Debug.LogWarning("Card not found in cache: " + card.Name);
        return null;
    }
}



