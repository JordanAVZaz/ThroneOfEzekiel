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
    public int indexNumber = 0; 

    public Card Create_UnitCard()
    {
        GameObject unitcardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        if (unitcardPrefab == null)
        {
            UnityEngine.Debug.LogError("Card prefab not found!");
            return null;
        }

        GameObject unitcard = UnityEngine.Object.Instantiate(unitcardPrefab);
        UnitCard unitCardComponent = unitcard.GetComponent<UnitCard>();
        unitcard.SetActive(false);
        if (unitCardComponent == null)
        {
            UnityEngine.Debug.LogError("UnitCard component not found on the prefab!");
            return null;
        }
        unitCardComponent.Initialize(link, indexNumber);
        indexNumber++;
        UnityEngine.Debug.Log("Unit Card Created................" + name + ".Link: " + link);
        Card cardData = unitcard.GetComponent<Card>();
        return cardData;
    }
}

[Serializable]
public class Deck
{
    public string link;
    public string deckName;
    public List<CardEntry> metaDeck =  new List<CardEntry>();//stores card data
    //public List<GameObject> instantiatedCards = new List<GameObject>();//stores created cards
    public CardList deck = new CardList();

    public Deck()
    {

    }

    public Deck(string link)
    {
        LoadDeck(link);
    }

    public Card Draw()
    {
        if (deck.Count == 0)
        {
            UnityEngine.Debug.LogError("The deck is empty.");
            return null;
        }

        //int index = UnityEngine.Random.Range(0, instantiatedCards.Count);
        //GameObject drawnCard = instantiatedCards[index];
        //instantiatedCards.RemoveAt(index);
        //drawnCard.SetActive(true);
        //return drawnCard;
        Card drawnCard = deck[-1];
        drawnCard.gameObject.SetActive(true);
        return drawnCard;
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
        if (metaDeck == null || metaDeck.Count == 0)
        {
            UnityEngine.Debug.LogError("No cards to process...............");
            return;
        }

        foreach (var cardEntry in metaDeck)
        {
            UnityEngine.Debug.Log("processing................" + cardEntry.name + " x" + cardEntry.copies);

            for (int i = 0; i < cardEntry.copies ; i++)
            {
                UnityEngine.Debug.Log("looping................");
                Card newCard = null;
                newCard = cardEntry.Create_UnitCard();

                if (newCard != null)
                {
                    deck.Add(newCard);
                    UnityEngine.Debug.Log(cardEntry.name + " has been added to the deck");
                }
                else
                {
                    UnityEngine.Debug.LogError("null card detected");
                }
            }

            deck.Shuffle();
        }

    }
}
