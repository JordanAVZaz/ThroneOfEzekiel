using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : Singleton<GameBoard>
{
    public GameObject baseTilePrefab; // Drag your BaseTile prefab here in the Unity Editor.
    public BaseTile[,] _gameBoard { get; private set; } // Use a 2D array
    public List<BaseTile> _gameBoardLookUp;

    
    void Start()
    {
        int size = 5; // Size of the board (5x5 for a total of 25 tiles)
        _gameBoard = new BaseTile[size, size]; // Use a 2D array

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                // Instantiate a new tile from the prefab.
                GameObject tileObject = Instantiate(baseTilePrefab, new Vector3(col * 10, 0, row * 10), Quaternion.identity);
                BaseTile tile = tileObject.GetComponent<BaseTile>();

                // Initialize the tile's location and ID
                tile.Init(row, col);
                _gameBoardLookUp.Add(tile);

                if (row < 2)
                {
                    tile.playerX_summoningZone = 1;
                }
                else if (row > 2)
                {
                    tile.playerX_summoningZone = 2;
                }
                else
                    tile.playerX_summoningZone = 0;

                // Store the tile in our game board array.
                _gameBoard[row, col] = tile;
            }
        }
    }
    public BaseTile GetTile(Tuple<int, int> id)
    {
        BaseTile tile = _gameBoard[id.Item1, id.Item2];
        return tile;
    }

    public BaseTile GetTile(int x, int y)
    {
        BaseTile tile = _gameBoard[x, y];
        return tile;
    }

    public BaseTile FindTileOfSelectable(Card card)
    {

        foreach (BaseTile tile in _gameBoardLookUp)
        {
            if (tile.GetOccupyingCard == card)
            {
                return tile;
            }
        }
            Debug.LogError("Selectable card does not exist on the board");
            return null;
        
    }

    public BaseTile FindTileOfAll(Card card)
    {
        foreach (BaseTile tile in _gameBoardLookUp)
        {
            foreach (Card c in tile.deck)
            {
                if (c == card)
                {
                    return tile;
                }
            }
        }
        Debug.LogError("The card does not exist anywhere on the board");
        return null;
    }
}
