using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject baseTilePrefab; // Drag your BaseTile prefab here in the Unity Editor.
    private static BaseTile[,] gameBoard; // Use a 2D array

    void Start()
    {
        int size = 5; // Size of the board (5x5 for a total of 25 tiles).
        gameBoard = new BaseTile[size, size]; // Use a 2D array

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                // Instantiate a new tile from the prefab.
                GameObject tileObject = Instantiate(baseTilePrefab, new Vector3(col * 10, 0, row * 10), Quaternion.identity);
                BaseTile tile = tileObject.GetComponent<BaseTile>();

                // Initialize the tile's location and ID
                tile.Init(row, col);

                if (row < 2)
                {
                    tile.player_x_SummoningZone = 1;
                }
                else if (row > 2)
                {
                    tile.player_x_SummoningZone = 2;
                }
                else
                    tile.player_x_SummoningZone = 0;

                // Store the tile in our game board array.
                gameBoard[row, col] = tile;


            }
        }
    }
    public BaseTile GetTile(Tuple<int, int> id)
    {
        BaseTile tile = gameBoard[id.Item1, id.Item2];
        return tile;
    }

    void Update()
    {

    }
}
