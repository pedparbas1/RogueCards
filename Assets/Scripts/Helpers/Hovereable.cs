using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hovereable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool isHovered;
    public bool IsHovered 
    {
        get => isHovered;
        private set => isHovered = value;
    }

    public UnityEvent OnHover;
    public UnityEvent OnUnhover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover.Invoke();
        IsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnUnhover.Invoke();
        IsHovered = false;
    }
}
