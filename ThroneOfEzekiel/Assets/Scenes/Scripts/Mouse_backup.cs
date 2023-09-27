using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mouse : MonoBehaviour
{
    private int handLayer = 1 << 6;
    private int tileLayer = 1 << 7;
    private Vector3 originalScale;
    private GameObject previousObject; // This is for hover effect
    private GameObject selectedObject; // This is for selection effect/hover
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, handLayer) && GameState.Instance.State == GameState.Global_States.Idle)
        {
            GameObject objectHit = hit.transform.gameObject;
            Card cardAttributes = objectHit.GetComponent<Card>();

            //hover effect
            if (previousObject != objectHit && objectHit != selectedObject)
            {
                // Reset the scale of the previously hovered object
                if (previousObject != null && previousObject != selectedObject)
                {
                    previousObject.GetComponent<Card>().Scale_Card_Reset(); // Reset to original scale
                }
                // Store this object as the previous object and remember its original scale
                previousObject = objectHit;
                // Increase the scale for hover effect
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
                // sets first selectable object if selectObject=false 
                // Mark current object as selected
                cardAttributes.Selection(true);
                selectedObject = objectHit;
            }
        }
        //selection
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer) && GameState.Instance.State == GameState.Global_States.HandCardSelected)
        {
            GameObject objectHit = hit.transform.gameObject;
            BaseTile tileAttributes = objectHit.GetComponent<BaseTile>();
           
            if (previousObject != objectHit && objectHit != selectedObject)
            {
                // Reset the color of the previously hovered object
                if (previousObject != null && previousObject != selectedObject && previousObject.GetComponent<BaseTile>().IsHovered)
                {
                    previousObject.GetComponent<BaseTile>().Fill_Default_Color();
                }
                // Store this object as the previous object
                previousObject = objectHit;
                // Change the color for hover effect
                tileAttributes.Fill_Selectable_Color();
            }
         
        }

        //exit selected state
        if (GameState.Instance.State == GameState.Global_States.HandCardSelected)
        {
            //have to break out of layer, so command can be used gloabaly
            // Handle Deselection with Escape key
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                selectedObject.GetComponent<Card>().Selection(false);
                selectedObject = null;
            }
        }
        UnityEngine.Debug.Log(previousObject.name);
    }

    private void Hand_Interaction_Handler()
    {

    }
    private void Board_Interaction_Handler()
    {

    }
}
