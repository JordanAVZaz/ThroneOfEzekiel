using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private bool occupied = false;
    private bool augment = false;
    private Vector2 tileLocation;
    
    // Start is called before the first frame update
    public void Init(Vector2 location)
    {
        tileLocation = location;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOccupied { get; set; }
    public bool IsAugmented { get; set; }
    public Vector2 TileLocation { get; private set; }

}
