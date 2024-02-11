using System;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Singleton<Mouse>
{
    public delegate void MouseClickHandler(GameObject clickedObject);
    public event MouseClickHandler OnMouseClick;
    private int _handLayer = 1 << 6;
    private int _tileLayer = 1 << 7;
    private Camera _mainCamera;
    private InteractiveObject _focusedObject; //Tracks the last object the mouse interacted with: Hover or Click. 
    private InteractiveObject _selectedObject;//private T _selectedObject; //Stores the selected object
    private List<GameObject> _selected; // Can stores multiple selected objects
    private RulesToggle _rulesToggle;
   
    // go through _selectedObject to set
    public Card SelectedCard => AuxileryLogic.IsIt<Card>(_selectedObject) ? _selectedObject.GetComponent<Card>() : null;
    public BaseTile SelectedTile => AuxileryLogic.IsIt<BaseTile>(_selectedObject) ? _selectedObject.GetComponent<BaseTile>() : null;
    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        //Mouse RayCasts every frame.
        if (_mainCamera != null)
        {
            Mouse_RayCast();
            //Rules toggle.
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                RulesToggle.Instance.ToggleRulesVisibility();
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Main Camera Null");
        }
    }

    //Casts a Ray. Depending on Game State will
    private void Mouse_RayCast()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var hitObject = hit.collider.gameObject;
            var hitCard = hitObject.GetComponent<Card>();

            switch (GameState.Instance.State)
            {
                case GameState.Global_States.Idle:
                    if (hitCard is Card)
                    {
                        HandleCardInteraction(hitCard);
                    }
                    else
                    {
                        ResetPreviousObject();
                    }
                    break;

                case GameState.Global_States.HandCardSelected:
                    if ((1 << hitObject.layer) == _tileLayer)
                    {
                        HandleBoardInteraction(hitObject.GetComponent<InteractiveObject>());

                        if (Input.GetMouseButtonDown(0) && OnMouseClick != null)
                        {
                            OnMouseClick.Invoke(hitObject);
                            Deselect__selectedObject();
                        }
                    }
                    break;

                case GameState.Global_States.BoardCardSelected:
                    if ((1 << hitObject.layer) == _tileLayer)
                    {
                        //HandleBoardInteraction(hitObject);

                        if (Input.GetMouseButtonDown(0) && OnMouseClick != null)
                        {
                            Debug.Log($"{SelectedCard.Name} is selected");
                            OnMouseClick.Invoke(hitObject);
                            Deselect__selectedObject();
                            BoardVisualiser.Instance.ResetBoard();
                        }
                    }
                    break;

                default:
                    Debug.Log("Unhandled game state");
                    break;
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                Deselect__selectedObject();
                BoardVisualiser.Instance.ResetBoard();
            }
        }
    }

    //Handles Mouse and Card interactions
    private void HandleCardInteraction(Card hitCard)
    {
        //Null Check
        if (hitCard == null)
        {
            UnityEngine.Debug.LogError("No card component loaded on raycasted card");
            return;
        }
        //Hover Card
        if (_focusedObject != hitCard.gameObject && _selectedObject != hitCard)
        {
            ResetPreviousObject();
            _focusedObject = hitCard;
            hitCard.card3D.VisualizeHover();
        }
        //Select Card
        if (Input.GetMouseButtonDown(0))
        {
            if (AuxileryLogic.IsIt<Card>(_selectedObject))
            {
                ResetPreviousObject();
            }
            _selectedObject = hitCard;
            hitCard.Selection(true);
        }
    }
    // Handles Mouse and Board interactions
    private void HandleBoardInteraction(InteractiveObject hitObject)
    {
        if (_focusedObject != hitObject && hitObject is BaseTile)
        {
            ResetPreviousObject();
            _focusedObject = hitObject;
            _focusedObject.GetComponent<BaseTile>().tile3D.FillSelectableColor();
        }
    }
    // resets the previously selected gameobject: Card or Tile. 
    private void ResetPreviousObject()
    {
        if (_focusedObject == null) return;

        var card = _focusedObject.GetComponent<Card>();
        if (card != null)
        {
            if (card == _selectedObject)
            {
                card.card3D.ScaleReset();//should keep border until out of the gamestate
            }
            else
            {
                card.card3D.VisualizeDefault();
            }
        }
        else
        {
            var baseTile = _focusedObject.GetComponent<BaseTile>();
            if (baseTile != null)
            {
                baseTile.tile3D.FillDefaultColor();
            }
        }
    }
    //Deselects the selected card or tile
    private void Deselect__selectedObject()
    {
        if (_selectedObject is Card)
        {
            _selectedObject.GetComponent<Card>().Selection(false);
            _selectedObject = null;
        }
    }

}
