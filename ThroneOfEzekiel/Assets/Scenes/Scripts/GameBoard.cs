using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject baseTilePrefab; // Drag your BaseTile prefab here in the Unity Editor.
    private BaseTile[] gameBoard;

    void Start()
    {
        int size = 5; // Size of the board (5x5 for a total of 25 tiles).
        gameBoard = new BaseTile[size * size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                // Instantiate a new tile from the prefab.
                GameObject tileObject = Instantiate(baseTilePrefab, new Vector3(col*10, 0, row*10), Quaternion.identity);
                BaseTile tile = tileObject.GetComponent<BaseTile>();

                // Initialize the tile's location.
                tile.Init(new Vector2(row, col));

                // Store the tile in our game board array.
                gameBoard[row * size + col] = tile;
            }
        }
    }

    void Update()
    {

    }
}
