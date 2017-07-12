using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickListener : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Action<Vector2> ClickDownAction;
    public Action<Vector2> ClickUpAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ClickDownAction != null)
            ClickDownAction(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (ClickUpAction != null)
            ClickUpAction(Input.mousePosition);
    }
}
