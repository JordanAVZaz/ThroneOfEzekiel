using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class VisualizationManager : Singleton<VisualizationManager>
{
    private List<List<GameObject>> _allCards;
    private List<GameObject> _player1HandCards = new List<GameObject>();
    private List<GameObject> _player2HandCards = new List<GameObject>();
    private List<GameObject> _boardCards = new List<GameObject>();
    private List<GameObject> _removedCards = new List<GameObject>();

    protected override void Awake(){
        _allCards = new List<List<GameObject>>();
        _allCards.Add(_player1HandCards);
        _allCards.Add(_player2HandCards);
        _allCards.Add(_boardCards);
        _allCards.Add(_removedCards);
        base.Awake();
    }

    public void P1_Draw(GameObject card)
    {
        if (card.GetComponent<Card>() != null)
        {
            _player1HandCards.Add(card);
        }
        else { UnityEngine.Debug.LogError("Can't add game object without card component to _player1HandCards"); }
    }

    public void P2_Draw(GameObject card)
    {
        if (card.GetComponent<Card>() != null)
        {
            _player2HandCards.Add(card);
        }
        else { UnityEngine.Debug.LogError("Can't add game object without card component to _player2HandCards"); }

    }

    public void Play_Card(GameObject card)
    {
        if (card.GetComponent<Card>() != null)
        {
            _player2HandCards.Add(card);
        }
        else { UnityEngine.Debug.LogError("Can't add game object without card component to _player2HandCards"); }

    }

    public void Remove_Card(GameObject card)
    {
         if (card.GetComponent<Card>() != null)
        {
            card.SetActive(false);
            _player2HandCards.Add(card);
        }
        else { UnityEngine.Debug.LogError("Can't add game object without card component to _player2HandCards"); }
    }

}
