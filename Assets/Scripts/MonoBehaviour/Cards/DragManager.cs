using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Canvas canvas;
    public RectTransform rectTransform;

    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(eventData);
    }
}
