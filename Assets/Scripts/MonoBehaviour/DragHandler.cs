using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragHandler : MonoBehaviour
{
    [SerializeField] GameObject go;
    [SerializeField] FloatVariable interactingScale;
    [SerializeField] float MaxY = .5f;
    [SerializeField] EventManager eventManager;
    //private properties
    private Vector3 _scaleVector;
    private Vector3 _startingPosition;
    private bool selected = false;

    void OnEnable()
    {
        eventManager.OnBeginDragEvent += OnBeginDrag;
        eventManager.OnDragEvent += OnDrag;
        eventManager.OnEndDragEvent += OnEndDrag;
        eventManager.OnPointerEnterEvent += OnEnterHover;
        eventManager.OnPointerExitEvent += OnExitHover;
        _scaleVector = (Vector3.one + Vector3.back * 2) * interactingScale.Value;
    }

    private void OnEnterHover(PointerEventData data)
    {
        go.transform.localScale = _scaleVector;
        go.transform.localPosition += Vector3.back * 5;
    }
    private void OnExitHover(PointerEventData data)
    {
        go.transform.localScale = Vector3.one;
        go.transform.localPosition -= Vector3.back * 5;
    }

    public void OnBeginDrag(PointerEventData pointerEvent)
    {
        eventManager.OnPointerEnterEvent -= OnEnterHover;
        eventManager.OnPointerExitEvent -= OnExitHover;


        _startingPosition = pointerEvent.pointerDrag.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (selected) return;
        if(go.transform.position.y >= MaxY)
        {
            go.transform.localPosition = Vector3.zero + Vector3.up * 120;
            selected = true;
            return;
        }
        go.transform.position += (Vector3) eventData.delta ;
    }

    public void OnEndDrag(PointerEventData pointerEvent)
    {
        if (selected) return;
        go.transform.position = _startingPosition;

        eventManager.OnPointerEnterEvent += OnEnterHover;
        eventManager.OnPointerExitEvent += OnExitHover;
    }

    public void OnSelect(BaseEventData eventData)
    {
        go.transform.localScale = _scaleVector;
    }

    void OnDisable()
    {

    }
}
