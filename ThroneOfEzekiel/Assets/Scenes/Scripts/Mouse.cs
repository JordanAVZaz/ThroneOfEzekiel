using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mouse : MonoBehaviour
{
    private int handLayer = 1 << 6;
    private int tileLayer = 1 << 7;
    private Vector3 originalScale;
    private GameObject previousObject; // stores the previosly hovered object
    private GameObject selectedObject; // stores 

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleGameStateChange(GameState.Instance.State);
    }

    private void HandleGameStateChange(GameState.Global_States newState)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        switch (newState)
        {
            case GameState.Global_States.Idle:
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, handLayer))
                {
                    Hand_Interaction_Handler(hit);
                }
                else
                    Reset_Previous_Object();
                break;

            case GameState.Global_States.HandCardSelected:
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
                {
                    Board_Interaction_Handler(hit);
                }
                else
                    Reset_Previous_Object();

                // Handle Deselection with Escape key
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
                {
                    selectedObject.GetComponent<Card>().Selection(false);
                    selectedObject = null;
                }
                break;

            case GameState.Global_States.FieldCardSelected:
                // Add logic here if necessary.
                break;

            default:
                UnityEngine.Debug.Log("State Error");
                // Other state logic or do nothing.
                break;
        }
    }

    private void Reset_Previous_Object()
    {
        if (previousObject?.GetComponent<Card>() != null )
        {
            previousObject.GetComponent<Card>().Scale_Card_Reset();
        }
        else if (previousObject?.GetComponent<BaseTile>() != null)
        {
            previousObject.GetComponent<BaseTile>().Fill_Default_Color();
        }
    }

    private void Hand_Interaction_Handler(RaycastHit hit)
    {
        GameObject objectHit = hit.transform.gameObject;
        Card cardAttributes = objectHit.GetComponent<Card>();

        //hover effect
        //if this isnt the previouse object and the currently stored hovered object
        if (previousObject != objectHit && objectHit != selectedObject)
        {
            // Reset the scale of the previously hovered object
            //if previouse object isnt null and isn't the current hovered object
            if (previousObject?.GetComponent<Card>() != null && previousObject != selectedObject)
            {
                //resets its scale
                previousObject.GetComponent<Card>().Scale_Card_Reset();
            }

            // Store this object as the previous object and remember its original scale
            previousObject = objectHit;
            objectHit.GetComponent<Card>().Scale_Card();
        }

        // Handle Selection Effect
        if (Input.GetMouseButtonDown(0) && objectHit != selectedObject)
        {
            if (selectedObject)
            {
                // Deselect previously selected object
                selectedObject.GetComponent<Card>().Selection(false);
                selectedObject.GetComponent<Card>().Scale_Card_Reset();
            }

            // Mark current object as selected
            cardAttributes.Selection(true);
            selectedObject = objectHit;
        }
    }

    private void Board_Interaction_Handler(RaycastHit hit)
    {
        GameObject objectHit = hit.transform.gameObject;
        // checks that it hasn't already been hit 
        if (previousObject != objectHit)
        {
            // checks if its null and is a basetile
            if (previousObject != objectHit)
            {
                //returns it to default
                Reset_Previous_Object();
            }
        }
        // Store this object as the previous object
        previousObject = objectHit;
        previousObject.GetComponent<BaseTile>().Fill_Selectable_Color();
    }


    private void OnEnable()
    {
        GameState.OnGameStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameState.OnGameStateChanged -= HandleGameStateChange;
    }
}
