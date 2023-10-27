using UnityEngine;

public interface ISubscribeToMouseClicks 
{
    void OnMouseClick(GameObject clickedObject);

    void OnEnable();

    void OnDisable();
    /*
    {
    var mouse = FindObjectOfType<Mouse>();
    mouse.OnMouseClick -= OnMouseClick;
    }
    */
}
