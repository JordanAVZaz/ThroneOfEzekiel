using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardEntry
{
    public string name;
    public string type;
    public int copies;
    public string link;
    public static int indexNumber = 0;

    public Card Create_UnitCard()
    {
        GameObject unitcardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        if (unitcardPrefab == null)
        {
            UnityEngine.Debug.LogError("Card prefab not found for " + name);
            return null;
        }

        GameObject unitcard = UnityEngine.Object.Instantiate(unitcardPrefab);
        if (unitcard == null)
        {
            UnityEngine.Debug.LogError("Failed to instantiate card for " + name);
            return null;
        }

        UnitCard unitCardComponent = unitcard.GetComponent<UnitCard>();
        if (unitCardComponent == null)
        {
            UnityEngine.Debug.LogError("UnitCard component not found on the prefab for " + name);
            return null;
        }

        unitCardComponent.Initialize(link, indexNumber);
        unitcard.SetActive(false);
        indexNumber++;

        UnityEngine.Debug.Log("Unit Card Created for " + name + ". Link: " + link);
        return unitcard.GetComponent<Card>();
    }
    //must be called when finishing creation of a new deck
    public static void ResetIndex()
    {
        indexNumber = 0;
    }

}

[Serializable]
public class Deck
{
    public string link;
    public string deckName;
    public List<CardEntry> cards = new List<CardEntry>();//stores card data
    public CardList deck = new CardList();

    public Deck()
    { }

    public Deck(string link)
    {
        LoadDeck(link);
    }
    private void LoadDeck(string link)
    {
        TextAsset deckJson = Resources.Load<TextAsset>(link);
        if (deckJson == null)
        {
            UnityEngine.Debug.LogError("Could not load deck from: " + link);
            return;
        }

        JsonUtility.FromJsonOverwrite(deckJson.text, this);
        ProcessCards();
    }

    private void ProcessCards()
    {
        if (cards == null || cards.Count == 0)
        {
            UnityEngine.Debug.LogError("No cards to process.");
            return;
        }

        foreach (var cardEntry in cards)
        {
            if (cardEntry.copies <= 0)
            {
                UnityEngine.Debug.LogWarning(cardEntry.name + " has 0 or fewer copies. Skipping.");
                continue;
            }

            for (int i = 0; i < cardEntry.copies; i++)
            {
                Card newCard = cardEntry.Create_UnitCard();
                if (newCard != null)
                {
                    deck.Add(newCard);
                    UnityEngine.Debug.Log(cardEntry.name + " has been added to the deck. Copy " + (i + 1) + " of " + cardEntry.copies+" index number:" + newCard.indexID);
                }
                else
                {
                    UnityEngine.Debug.LogError("Failed to create card for " + cardEntry.name + ". Skipping this copy.");
                }
            }
        }

        UnityEngine.Debug.Log("Total cards in the deck after processing: " + deck.Count);
        //shuffles the deck
        deck.Shuffle();
        //Resets index for next deck load
        CardEntry.ResetIndex();

    }
}
