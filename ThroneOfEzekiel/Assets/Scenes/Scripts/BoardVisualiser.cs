using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardVisualiser : Singleton<BoardVisualiser>
{
    public VisualiseAttackPatterns attackPattern;

    void Awake()
    {
        base.Awake();
        attackPattern = new VisualiseAttackPatterns();
    }

    public void AttackPattern(UnitPropertys.Unit_Class unit_Class, BaseTile tile)
    {
        if (attackPattern == null)
        {
            Debug.LogError("AttackPattern instance is null.");
            return;
        }
        if (tile == null)
        {
            Debug.LogError("Tile parameter is null.");
            return;
        }

        // If both checks pass, we can safely proceed.
        attackPattern.AttackPatterns(unit_Class, tile);
    }

    public void ResetBoard()
    {
        foreach (BaseTile tile in GameBoard.Instance._gameBoardLookUp)
        {
            tile.tile3D.FillDefaultColor();
        }
    }
}
