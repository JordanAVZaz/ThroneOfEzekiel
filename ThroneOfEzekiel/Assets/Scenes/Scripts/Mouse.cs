using System;
using UnityEngine;

public class Mouse : Singleton<Mouse>
{
    public delegate void MouseClickHandler(GameObject clickedObject);
    public event MouseClickHandler OnMouseClick;

    private int _handLayer = 1 << 6;
    private int _tileLayer = 1 << 7;
    private Camera _mainCamera;
    private GameObject _previousObject;
    public Card selectedCard { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_mainCamera != null)
        {
            HandleMouseEvent();
        }
        else
        {
            UnityEngine.Debug.LogError("Main Camera Null");
        }
    }

    private void HandleMouseEvent()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var hitObject = hit.collider.gameObject;
            var hitCard = hitObject.GetComponent<Card>();

            switch (GameState.Instance.State)
            {
                case GameState.Global_States.Idle:
                    if ((1 << hitObject.layer) == _handLayer)
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
                        HandleBoardInteraction(hitObject);

                        if (Input.GetMouseButtonDown(0) && OnMouseClick != null)
                        {
                            OnMouseClick.Invoke(hitObject);
                            DeselectCard();
                        }
                    }
                    else
                    {
                        //ResetPreviousObject();
                    }
                    break;

                default:
                    Debug.Log("Unhandled game state");
                    break;
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                DeselectCard();
            }
        }
    }
    private void HandleCardInteraction(Card hitCard)
    {
        if (hitCard == null)
        {
            UnityEngine.Debug.LogError("No card component loaded on raycasted card");
            return;
        }

        if (_previousObject != hitCard.gameObject && selectedCard != hitCard)
        {
            ResetPreviousObject();
            _previousObject = hitCard.gameObject;
            hitCard.card3D.VisualizeHover();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedCard != null)
            {
                //DeselectCard();
                ResetPreviousObject();
            }
            selectedCard = hitCard;
            hitCard.Selection(true);
        }
    }

    private void HandleBoardInteraction(GameObject hitObject)
    {
        if (_previousObject != hitObject)
        {
            ResetPreviousObject();
            _previousObject = hitObject;
            hitObject.GetComponent<BaseTile>().Fill_Selectable_Color();
        }
    }

    private void ResetPreviousObject()
    {
        if (_previousObject == null) return;

        var card = _previousObject.GetComponent<Card>();
        if (card != null)
        {
            if (card == selectedCard)
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
            var baseTile = _previousObject.GetComponent<BaseTile>();
            if (baseTile != null)
            {
                baseTile.Fill_Default_Color();
            }
        }
    }

    private void DeselectCard()
    {
        if (selectedCard != null)
        {
            selectedCard.Selection(false);
            selectedCard = null;
        }
    }

    public Card SelectedCard
    {
        get => selectedCard;
        set => selectedCard = value;
    }
}
