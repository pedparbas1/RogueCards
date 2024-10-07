using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardPile : MonoBehaviour, IDropHandler
{
    [SerializeField] private HandManager HM;

    void Start()
    {
        HM = HandManager.Instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(HM);
        CardInstance c = eventData.selectedObject.GetComponent<CardInstance>();
        if(c != null)
        {
            Debug.Log(HandManager.Instance.hand);
            HandManager.Instance.PlayerDiscard(c);
        }
    }
}
