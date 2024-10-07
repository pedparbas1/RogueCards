using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        private set => isSelected = value;
    }
    
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log(eventData.button.ToString());
        if(eventData.button.Equals(PointerEventData.InputButton.Right))
        {
            Deselect();
            return;
        }
        OnSelect?.Invoke();    
        IsSelected = true;
    }

    public void Deselect()
    {
        // Debug.Log("Deselect");
        OnDeselect?.Invoke();
        IsSelected = false;
    }
}
