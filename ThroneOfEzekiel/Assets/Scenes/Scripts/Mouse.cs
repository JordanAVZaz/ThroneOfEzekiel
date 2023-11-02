using System;
using UnityEngine;

public class Mouse : Singleton<Mouse>
{
    public delegate void MouseClickHandler(GameObject clickedObject);
    public event MouseClickHandler OnMouseEvent;
    private int _handLayer = 1 << 6;
    private int _tileLayer = 1 << 7;
    private Camera _mainCamera;
    private GameObject _previousObject;
    public GameObject eventObject { get; private set; }
    public GameObject target;

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
        Debug.Log("Awake called on Mouse");
    }

    void Update()
    {
        if (_mainCamera != null)
        {
            HandleMouseEvent();
            HandleKeys();
            UnityEngine.Debug.Log("update");

        }
        else
        {
            UnityEngine.Debug.LogError("Main Camera Null");
        }
    }

    private void HandleMouseEvent()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _handLayer | _tileLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != eventObject)
            {
                // Reset eventObject if we have moved to a new object
                ResetEventObject();
                eventObject = hitObject;

                var card = eventObject.GetComponent<Card>();
                var tile = eventObject.GetComponent<BaseTile>();
                UnityEngine.Debug.Log("Hit");
                // Only one of these should be non-null given an object can't be both a Card and a Tile
                if (card != null)
                {
                    if (Input.GetMouseButtonDown(0) && card.ISelectable)
                    {
                        card.Select();
                    }
                    else if (card.IHover)
                    {
                        card.card3D.ScaleCard();
                    }
                }
                else if (tile != null)
                {
                    if (tile.IHover)
                    {
                        tile.Fill_Selectable_Color();
                    }
                    if (Input.GetMouseButtonDown(0) && tile.ITarget != true)
                    {
                        //migrates card to the tile
                        tile.MigrateCard(eventObject.GetComponent<Card>());
                        eventObject = null;
                    }
                }
            }
        }
    }


    private void HandleKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (eventObject != null && eventObject.GetComponent<Card>() != null)
            {
                eventObject.GetComponent<Card>().card3D.VisualizeReset();
                eventObject = null;
            }
        }
    }

    private void ResetEventObject()
    {
        if (eventObject != null)
        {
            if (eventObject.GetComponent<Card>() != null)
            {
                eventObject.GetComponent<Card>().card3D.VisualizeReset();
            }
            else if (eventObject.GetComponent<BaseTile>() != null)
            {
                eventObject.GetComponent<BaseTile>().Fill_Default_Color();
            }
            else
            {
                //something else in the future
            }
        }
    }
}