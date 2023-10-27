using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CardList : List<Card>
{
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
}



