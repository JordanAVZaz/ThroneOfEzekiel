using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualiseAttackPatterns
{
    private const int GridSize = 5; // Assuming a 5x5 grid

    public void AttackPatterns(UnitPropertys.Unit_Class unit_Class, BaseTile tile)
    {
        // Clear previous patterns if needed
        ClearAllPatterns();

        // Visualize patterns based on unit class
        switch (unit_Class)
        {
            case UnitPropertys.Unit_Class.Magic:
            case UnitPropertys.Unit_Class.Bow:
                // These attacks can target all tiles in the same row and column
                VisualizeLinearPattern(tile, ignoreUnits: true);
                break;
            case UnitPropertys.Unit_Class.Sword:
                // Sword attack can target adjacent non-diagonal tiles and two tiles forward
                VisualizeAdjacentPattern(tile);
                VisualizeTileInFront(tile, 2, ignoreUnits: false); // Two tiles forward
                break;
            case UnitPropertys.Unit_Class.Lance:
                // Lance attack is similar to Sword, but with an additional tile forward
                VisualizeAdjacentPattern(tile);
                VisualizeTileInFront(tile, 3, ignoreUnits: false); // Three tiles forward
                break;
            case UnitPropertys.Unit_Class.Ruler:
                // Define ruler attack pattern if it's different
                break;
        }
    }

    private void VisualizeAdjacentPattern(BaseTile tile)
    {
        var gridID = tile.gridID;
        int row = gridID.Item1;
        int col = gridID.Item2;

        // Visualize each direction, if no unit is present or blocking the way
        VisualizeTileIfAvailable(row + 1, col, false); // Up
        VisualizeTileIfAvailable(row - 1, col, false); // Down
        VisualizeTileIfAvailable(row, col + 1, false); // Right
        VisualizeTileIfAvailable(row, col - 1, false); // Left
    }

    private void VisualizeLinearPattern(BaseTile tile, bool ignoreUnits)
    {
        var gridID = tile.gridID;
        int row = gridID.Item1;
        int col = gridID.Item2;

        // Visualize all tiles in the same row and column
        for (int i = 0; i < GridSize; i++)
        {
            if (i != col) VisualizeTileIfAvailable(row, i, ignoreUnits);
            if (i != row) VisualizeTileIfAvailable(i, col, ignoreUnits);
        }
    }

    private void VisualizeTileInFront(BaseTile tile, int distance, bool ignoreUnits)
    {
        var gridID = tile.gridID;
        int row = gridID.Item1;

        // Visualize tiles 'distance' steps in front of the given tile
        for (int i = 1; i <= distance; i++)
        {
            int targetRow = row + i; // Assuming 'forward' is in the positive row direction
            VisualizeTileIfAvailable(targetRow, gridID.Item2, ignoreUnits);
        }
    }

    private void VisualizeTileIfAvailable(int row, int col, bool ignoreUnits)
    {
        if (IsWithinBounds(row, col))
        {
            BaseTile targetTile = GameBoard.Instance.GetTile(row, col);

            // If we need to check for units and there is one, mark as non-selectable
            if (!ignoreUnits && IsUnitPresent(targetTile))
            {
                targetTile.tile3D.FillNonSelectableColor();
            }
            else
            {
                targetTile.tile3D.FillSelectableColor();
            }
        }
    }

    private bool IsWithinBounds(int row, int col)
    {
        return row >= 0 && row < GridSize && col >= 0 && col < GridSize;
    }

    private bool IsUnitPresent(BaseTile tile)
    {
        // Implementation based on your game's logic for unit presence
        return tile.deck.Count > 0;
    }

    private void ClearAllPatterns()
    {
        // Clear all existing patterns on the board
        foreach (var tile in GameBoard.Instance._gameBoard)
        {
            tile.tile3D.FillDefaultColor();
        }
    }
}
