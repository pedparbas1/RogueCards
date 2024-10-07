using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerEvent
{
    public Action<PointerEventData> OnBeginDragEvent;
    public Action<PointerEventData> OnDragEvent;
    public Action<PointerEventData> OnEndDragEvent;
    public Action<PointerEventData> OnPointerClickEvent;
    public Action<PointerEventData> OnPointerEnterEvent;
    public Action<PointerEventData> OnPointerExitEvent;

    public int pointerId => throw new NotImplementedException();

    public string pointerType => throw new NotImplementedException();

    public bool isPrimary => throw new NotImplementedException();

    public int button => throw new NotImplementedException();

    public int pressedButtons => throw new NotImplementedException();

    public Vector3 position => throw new NotImplementedException();

    public Vector3 localPosition => throw new NotImplementedException();

    public Vector3 deltaPosition => throw new NotImplementedException();

    public float deltaTime => throw new NotImplementedException();

    public int clickCount => throw new NotImplementedException();

    public float pressure => throw new NotImplementedException();

    public float tangentialPressure => throw new NotImplementedException();

    public float altitudeAngle => throw new NotImplementedException();

    public float azimuthAngle => throw new NotImplementedException();

    public float twist => throw new NotImplementedException();

    public Vector2 tilt => throw new NotImplementedException();

    public PenStatus penStatus => throw new NotImplementedException();

    public Vector2 radius => throw new NotImplementedException();

    public Vector2 radiusVariance => throw new NotImplementedException();

    public EventModifiers modifiers => throw new NotImplementedException();

    public bool shiftKey => throw new NotImplementedException();

    public bool ctrlKey => throw new NotImplementedException();

    public bool commandKey => throw new NotImplementedException();

    public bool altKey => throw new NotImplementedException();

    public bool actionKey => throw new NotImplementedException();

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

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}