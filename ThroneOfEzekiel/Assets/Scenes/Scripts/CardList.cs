using System;
using System.Collections.Generic;
using UnityEngine;

public class CardList : List<Card>
{
    public CardList() : base() { }

    public new void Add(Card card)
    {
        //Debug.Log($"collection size{this.Count}");
        base.Add(card);
        card.indexID = this.Count - 1;
    }

    public bool RemoveCard(Card card)
    {
        int id = card.indexID;

        if (id >= 0 && id < this.Count)
        {
            base.RemoveAt(id); // Removes the card based on its indexID.
            UpdateIndices(id); // Update the indices of subsequent cards.
            return true;
        }
        return false; // Return false if the card couldn't be removed.
    }

    private void UpdateIndices(int start = 0)
    {
        //Debug.Log("refactoring ids");
        for (int i = start; i < this.Count; i++)
        {
            //if i was 5: this.[5].indexID = 5
            //shifting from the indexID < this way from 5 to macth the list.
            this[i].indexID = i;
            //Debug.Log($"{this[i]} id is :{i}");

        }
    }

    public bool MigrateCardTo(Card card, CardList newCollection)
    {
        if (card == null)
        {
            Debug.LogError("Attempted to migrate a null card.");
            return false;
        }

        if (!this.Remove(card)) // Try to remove the card from the current list.
        {
            Debug.LogError($"Attempted to migrate a card not present in the list: {card.name}");
            return false;
        }

        // Now that the card has been removed, it is safe to add it to the new list.
        newCollection.Add(card);


        // Debug logs to verify the operation, you can remove these once you're confident it works correctly.
        Debug.Log($"{card.name} has been moved from the old list to the new list.");

        return true; // Return true to indicate the migration was successful.
    }

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

            // Keeping indexID consistent with the position
            this[k].indexID = k;
            this[n].indexID = n;
        }
    }

    public int HowManyCopies(Card card)
    {
        // Count and return the number of copies without using a cache
        int copies = 0;
        foreach (Card c in this)
        {
            if (card.Name == c.Name)
            {
                copies++;
            }
        }
        return copies;
    }
}
