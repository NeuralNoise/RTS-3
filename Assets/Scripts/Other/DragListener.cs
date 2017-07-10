using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DragListener : IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action BeginDragAction;
    public Action DragAction;
    public Action EndDragAction;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (BeginDragAction != null)
            BeginDragAction();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragAction != null)
            DragAction();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (EndDragAction != null)
            EndDragAction();
    }
}
