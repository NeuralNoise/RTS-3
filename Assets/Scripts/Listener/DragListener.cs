using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DragListener : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action BeginDragAction;
    public Action<Vector2> DragAction;
    public Action EndDragAction;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (BeginDragAction != null)
            BeginDragAction();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragAction != null)
            DragAction(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (EndDragAction != null)
            EndDragAction();
    }
}
