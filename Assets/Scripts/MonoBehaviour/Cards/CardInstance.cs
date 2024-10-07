using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInstance : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GraphicRaycaster raycaster;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [Header("State")]
    public bool isSelected;
    public bool isHovered;
    public bool isDragging;

    [Header("Interaction Variables")]
    public FloatVariable selectionOffsetY;
    public FloatVariable hoverOffsetY;
    // public FloatVariable selectionScale;

    [SerializeField] float SelectionYOffset = 40;
    [SerializeField] float HoveringYOffset = 20;

    public bool isQueued = false;
    public void Enqueue() 
    {
        isQueued = true;
        if(isDragging) return;
    }
    public void Dequeue()
    {
        isQueued = false;
        if(isSelected) return;
        MoveTo(Vector2.zero);
    }

    private Vector2 offset; 

    private List<CardUpgrade> cardUpgrades;
    private RectTransform _rt;
    // [SerializeField] GameObject selector;

    [SerializeField] private CardEffect cardEffect;
    public CardEffect CardEffect
    {
        get => cardEffect;
        set
        {
            cardEffect = value;
            OnChangeEvent?.Invoke(this);
        }
    }

    //Events>
    [HideInInspector] public UnityEvent<CardInstance> OnBeginDragEvent;
    [HideInInspector] public UnityEvent<CardInstance> OnEndDragEvent;
    [HideInInspector] public UnityEvent<CardInstance> OnDragEvent;
    [HideInInspector] public UnityEvent<CardInstance> OnPointerEnterEvent;
    [HideInInspector] public UnityEvent<CardInstance> OnPointerExitEvent;
    [HideInInspector] public UnityEvent<CardInstance> OnPointerClickEvent;
    [HideInInspector] public UnityEvent<CardInstance> OnDestroyEvent;

    [HideInInspector] public event Action<CardInstance> OnChangeEvent;

    void Awake()
    {
        //SetUP global variables
        SelectionYOffset = selectionOffsetY?
            selectionOffsetY.Value : SelectionYOffset;
        HoveringYOffset = hoverOffsetY?
            hoverOffsetY.Value : HoveringYOffset;

        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
    }
    
    public virtual void PlayCard() {}
    

    public virtual void AddUpgrade(CardUpgrade cardUpgrade)
    {
        if (cardUpgrades.Count == 0) cardUpgrades = new();
        cardUpgrades.Add(cardUpgrade);

        OnChangeEvent?.Invoke(this);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        OnDragEvent.Invoke(this);

        Vector2 draggingPosition = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position) - offset;

        transform.position = new Vector3(draggingPosition.x, draggingPosition.y);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        OnBeginDragEvent.Invoke(this);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2) transform.position;

        eventData.selectedObject = this.gameObject;
        canvasGroup.blocksRaycasts = false;
        Select();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent.Invoke(this);
        isDragging = false;
        canvasGroup.blocksRaycasts = true;

        if(isSelected || isQueued) 
        {
            MoveTo(Vector3.zero + transform.up * SelectionYOffset);
            return;
        }
        
        MoveTo(Vector3.zero);
        eventData.selectedObject = null;
        isSelected = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        // canvas.overrideSorting = true;

        if(isSelected || isQueued) return;

        MoveTo(transform.up * HoveringYOffset);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        // canvas.overrideSorting = false;
        if(isSelected || isQueued) return;
        
        MoveTo(Vector3.zero);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent.Invoke(this);
        
        eventData.selectedObject = this.gameObject;
        Select();
    }

    public void OnDestroy()
    {
        OnDestroyEvent.Invoke(this);
    }

    public void Select()
    {
        isSelected = true;
        canvas.overrideSorting = true;
        MoveTo(Vector3.zero + transform.up * SelectionYOffset);
    }

    public void Deselect(bool ignoreQueue)
    {
        if(!isSelected) return ;
        
        isSelected = false;

        if (ignoreQueue) Dequeue();
        if(!isQueued)
            MoveTo(Vector3.zero);
        if (canvas) canvas.overrideSorting = false;
    } 

    private void MoveTo(Vector3 position)
    {
        if(isDragging) return;
        transform.localPosition = position;
    }
}