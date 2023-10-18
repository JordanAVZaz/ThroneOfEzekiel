using System;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Delegate and event for mouse click
    public delegate void MouseClickHandler(GameObject clickedObject);
    public event MouseClickHandler OnMouseClick;
    private int handLayer = 1 << 6;
    private int tileLayer = 1 << 7;
    private Camera mainCamera;
    private GameObject previousObject;
    private GameObject selectedObject;

      void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found");
        }
    }

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is null. Please ensure a camera is tagged as \"MainCamera\" in the scene.");
            return;
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            HandleMouseEvent();
        }
    }

    private void HandleMouseEvent()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            switch (GameState.Instance.State)
            {
                case GameState.Global_States.Idle:
                    if ((1 << hit.collider.gameObject.layer) == handLayer)
                    {
                        HandleHandInteraction(hit);
                    }
                    else
                    {
                        ResetPreviousObject();
                    }
                    break;

                case GameState.Global_States.HandCardSelected:
                    if ((1 << hit.collider.gameObject.layer) == tileLayer)
                    {
                        HandleBoardInteraction(hit);
                    }
                    else
                    {
                        ResetPreviousObject();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
                    {
                        DeselectObject();
                    }
                    break;

                // Add more cases as necessary

                default:
                    Debug.Log("Unhandled game state");
                    break;
            }

            // Trigger the mouse click event if any object is clicked
            if (Input.GetMouseButtonDown(0) && OnMouseClick != null)
            {
                OnMouseClick.Invoke(hit.collider.gameObject);
            }
        }
    }

    private void HandleHandInteraction(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;
        if (previousObject != hitObject && hitObject != selectedObject)
        {
            ResetPreviousObject();
            previousObject = hitObject;
            // Assume Scale_Card() is a method in your Card component or similar
            hitObject.GetComponent<Card>().Scale_Card();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject != null)
            {
                DeselectObject();
            }

            selectedObject = hitObject;
            hitObject.GetComponent<Card>().Selection(true);
        }
    }

    private void HandleBoardInteraction(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;
        if (previousObject != hitObject)
        {
            ResetPreviousObject();
            previousObject = hitObject;
            hitObject.GetComponent<BaseTile>().Fill_Selectable_Color();
        }
    }

    private void ResetPreviousObject()
    {
        if (previousObject != null)
        {
            var card = previousObject.GetComponent<Card>();
            if (card != null)
            {
                card.Scale_Card_Reset();
            }

            var baseTile = previousObject.GetComponent<BaseTile>();
            if (baseTile != null)
            {
                baseTile.Fill_Default_Color();
            }
        }
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Card>().Selection(false);
            selectedObject.GetComponent<Card>().Scale_Card_Reset();
            selectedObject = null;
        }
    }
    public GameObject SelectedObject
    {
        get => selectedObject;
        set => selectedObject = value;
    }
}
