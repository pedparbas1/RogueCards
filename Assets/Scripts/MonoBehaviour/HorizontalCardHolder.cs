using System;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCardHolder : MonoBehaviour
{
    [SerializeField] IntVariable MaxCardsInHand;
    [SerializeField] GameObject slotPrefab; 

    public CardInstance selectedCard, lastSelected;
    [SerializeField] CardInstanceVariable selectedCardVariable;

    private int selectedCardIndex;

    private CardInstance hoveredCard;
    private bool isCrossing;
    private int cardsDrawn = 0;
    public List<CardInstance> cards = new();

    [SerializeField] private Neighbours neighbours;

    void Start()
    {
        selectedCardVariable.Value = null;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1))
        {
            OnDeselect();
        }

        if(!selectedCard) return;

    }

    public void AddCardToHand(CardEffect c)
    {
        if(!c) return;
        cardsDrawn++;
        GameObject go = Instantiate(slotPrefab, transform);
        CardInstance card = Instantiate(c.cardVisual, go.transform);
        card.name = $"{cardsDrawn}: " + c.CardName;
        card.CardEffect = c;
        Subscribe(card);
        cards.Add(card);
    }

    void OnDrag(CardInstance c)
    {
        if(isCrossing) return;
        if(selectedCard.transform.position.x < neighbours.left?.Value.position.x)
        {
            Swap(neighbours.left);
            return;
        }

        if (selectedCard.transform.position.x > neighbours.right?.Value.position.x)
        {
            Swap(neighbours.right); 
        }
    }

    private void Swap(KeyValue<int, Transform> kv)
    {
        isCrossing = true;
        Transform parent = kv.Value.parent;
        Vector2 localPosition = kv.Value.localPosition;
        Debug.Log(localPosition);
        kv.Value.SetParent(selectedCard.transform.parent);
        kv.Value.localPosition = localPosition;
        Debug.Log(kv.Value.localPosition);
        selectedCard.transform.SetParent(parent);

        (cards[selectedCardIndex], cards[kv.Key]) =
        (cards[kv.Key], cards[selectedCardIndex]);

        GetNeighbours();
        isCrossing = false;
    }

    void OnBeginDrag(CardInstance c)
    {
        OnSelect(c);
    }

    void OnHover(CardInstance c) 
    {
        hoveredCard = c;
    }

    void OnUnhover(CardInstance c)
    {
        hoveredCard = null;
    }

    void OnSelect(CardInstance c)
    {
        Debug.Log(c.CardEffect.CardName);
        selectedCard = selectedCardVariable.Value = c;
        if(lastSelected != c)
        {
            lastSelected?.Deselect(false);
        } 
        lastSelected = c;
        GetNeighbours();
    }
    
    void OnDeselect()
    {
        lastSelected?.Deselect(true);
        selectedCardVariable.Value = null;
    }

    void OnDestroyCard(CardInstance c)
    {
        // Debug.Log("FullList");
        // cards.ForEach(x => Debug.Log(x.name));
        selectedCard = lastSelected = selectedCardVariable.Value = null;
        neighbours = new();
        cards.Remove(c);
        // Debug.Log("ActualList");
        // cards.ForEach(x => Debug.Log(x.name));
    }

    void GetNeighbours()
    {
        // Debug.Log(neighbours.ToString());
        if(cards.Count < 1) return;

        if(cards.Count == 1) 
        {
            selectedCardIndex = 0;
            neighbours = new(selectedCard.transform, selectedCardIndex,
                             selectedCard.transform, selectedCardIndex);
            return;
        }
        
        if(cards[0] == selectedCard)
        {
            selectedCardIndex = 0;
            neighbours = new(selectedCard.transform, selectedCardIndex,
                             cards[1].transform, 1);
            return;
        }

        if(cards[^1] == selectedCard)
        {
            selectedCardIndex= cards.Count - 1;
            neighbours = new(cards[selectedCardIndex-1].transform, selectedCardIndex-1, selectedCard.transform, selectedCardIndex);
            return;
        }

        for(int i = 1; i < cards.Count - 1; i++)
        {
            if(cards[i] == selectedCard)
            {
                selectedCardIndex = i;
                neighbours = new(cards[i-1]?.transform ,i-1 ,
                                 cards[i+1]?.transform, i+1);
                return;
            }
        }
    }
    
    void Subscribe(CardInstance c)
    {
        c.OnPointerClickEvent.AddListener(OnSelect);
        c.OnPointerEnterEvent.AddListener(OnHover);
        c.OnPointerExitEvent.AddListener(OnUnhover);
        c.OnBeginDragEvent.AddListener(OnBeginDrag);
        c.OnDragEvent.AddListener(OnDrag); 
        c.OnDestroyEvent.AddListener(OnDestroyCard);
    }

    [Serializable]
    private struct Neighbours
    {
        public KeyValue<int, Transform> left, right;
        public Neighbours(Transform left, int l, Transform right, int r)
        {
            this.left = new KeyValue<int, Transform>(l, left) ;
            this.right = new KeyValue<int, Transform>(r, right);
        }

        public override string ToString()
        {
            return $"({left?.Value.gameObject.name}, {right?.Value.gameObject.name})";
        }
    }
}
