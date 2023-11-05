using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GlobalPlayerManager : Singleton<GlobalPlayerManager>
{
    private Deck _p1Deck;
    private Deck _p2Deck;
    public Player player1;
    public Player player2;
    //public CardList _removedCards = new CardList();
    protected override void Awake()
    {
        base.Awake();
        player1 = new Player(GameState.Players.Player1, "JSON/Decks/Radiant_Starter");
        player2 = new Player(GameState.Players.Player2, "JSON/Decks/Dark_Starter");
    }
    private T PlayerMethod<T>(T player1, T player2)
    {
        if (GameState.Instance._playerTurn == GameState.Players.Player1)
        {
            return player1;
        }
        else if (GameState.Instance._playerTurn == GameState.Players.Player2)
        {
            return player2;
        }
        else
        {
            UnityEngine.Debug.LogError("Deck manager, Players are out of scope");
            return default(T);
        }
    }
    //Draw specifacly puts top deck card into hand CardList.
    //This is the first time that the 3d mesh will be visible
    //deck to hand migration must go through this channel

    public void SetActiveHand()
    {
        if (GameState.Instance._playerTurn == GameState.Players.Player1)
        {
            player1.hand.SetHandSleep(false);
            player2.hand.SetHandSleep(true);
        }
        else if (GameState.Instance._playerTurn == GameState.Players.Player2)
        {
            player1.hand.SetHandSleep(true);
            player2.hand.SetHandSleep(false);
        }
    }
    public Player GetActivePlayer()
    {
        return PlayerMethod(player1, player2);
    }
    public void Draw()
    {
        PlayerMethod(player1, player2).Draw();
    }

    public CardList GetActiveDeck()
    {
        return PlayerMethod(player1.deck, player2.deck);
    }

    public CardList GetActiveHand()
    {
        return PlayerMethod(player1.hand.cardsInHand, player2.hand.cardsInHand);
    }

    public CardList GetActiveBoardCards()
    {
        return PlayerMethod(player1.boardCards, player2.boardCards);
    }
    // Cards on the board must be current, so new card list is created
    // if using multiple times, caller must cache this state
    public CardList GetAllBoardCards()
    {
        CardList allCards = new CardList();
        allCards.AddRange(player1.boardCards);
        allCards.AddRange(player2.boardCards);
        return allCards;
    }


}
