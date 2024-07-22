using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInstance : MonoBehaviour, ISelectHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private CardEffect baseCard;

    private Effect cardInstance;
    private List<CardUpgrade> cardUpgrades;
    public void AddUpgrade(CardUpgrade cardUpgrade)
    {
        if (cardUpgrades.Count == 0) cardUpgrades = new();
        cardUpgrades.Add(cardUpgrade);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    void OnEnable()
    {
        cardInstance = baseCard.Effect;
    }

}
