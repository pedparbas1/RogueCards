using System;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;
    public InitialDeckVariable currentDeck;
    public CardInstanceVariable selectedCard;

    [Header("Relevant prefabs")]
    public GameObject HandParentGO;
    public GameObject drawPileGO;
    public GameObject discardPileGO;
    public HorizontalCardHolder cardHolder;

    [Header("Initial hand info")]
    [SerializeField] IntVariable initialCardsVariable;
    [SerializeField] int initialCards = 3;
    [SerializeField] IntVariable maxCardsInHand; 
    [SerializeField] int MaxHandSize = 5;
    [SerializeField] EnemyDataVariable heroData;
    [SerializeField] AbilitieScoreType initialCardsAbility;
    [SerializeField] int drawPosibilities = 2;

    [Header("Events")]
    public GameEvent OnDiscard;
    public GameEvent OnPlayCard;
    public IntGameEvent OnRequestCard, OnCardPlayedStamina, OnCardRequested;
    public StringEvent OnError;

    [Space]
    [Header("Decks")]
    public Deck hand;
    public Deck drawPile;
    public Deck discardPile;

    private int mod;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    } 

    void Start()
    {
        initialCards = initialCardsVariable? initialCardsVariable.Value : initialCards;
        MaxHandSize = maxCardsInHand.Value;
        drawPile = currentDeck.Value.Deck;
        this.hand = new(); 

        mod = heroData.Value.statBlockInstance.Stats.Stats.GetModifier(initialCardsAbility);
        initialCards += mod;
        drawPosibilities += mod;
        OnCardRequested.Raise(drawPosibilities);
        Debug.Log($"InitialCards : {initialCards}");
        RequestCards(initialCards);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRequestCard();
        }
    }

    public void PlayerRequestCard()
    {
        if(drawPosibilities < 1)
        {
            OnError?.Raise("Discard two cards to obtain more Draws");
            return;
        }
        drawPosibilities -= 1;
        OnCardRequested.Raise(drawPosibilities);
        RequestCards(1);
    }
    
    public void NewRoundDeal()
    {
        RequestCards(Math.Max(1, mod));
    }
    //TODO: Add Draw Card animation trigger
    public void RequestCards(int cardsToDeal)
    {
        List<string> cardsRetrieved = new();
        for(int i = 0; i < cardsToDeal; i++)
        {
            CardEffect effect = Deal();

            if(!effect)
            {
                OnError.Raise("No more cards in deck");
                return;
            } 

            if(hand.Size == MaxHandSize)
            {
                OnError.Raise($"Maximum number of cards in hand - Discarding {effect.CardName}");
                discardPile.Add(effect);
                CheckForDrawUpdate();

            } else {

                hand.Add(effect);
                cardHolder.AddCardToHand(effect);

                cardsRetrieved.Add(effect.name);

            }

        }
        OnRequestCard.Raise(drawPile.Size);
        // cardsRetrieved.ForEach(x => Debug.Log(x + " "));
    }

    CardEffect Deal()
    {
        if (drawPile.Size == 0)
        {
            DiscardPileToDrawPile();
            discardPile = new();
        }

        CardEffect effect = drawPile.RetrieveRandomCard();
        return effect;
    }

    void DiscardPileToDrawPile()
    {
        // Debug.Log("Recharging drawPile");
        drawPile.Add(discardPile);
        discardPile = new();
    }

    int totalDiscards = 0;

    public void CheckForDrawUpdate()
    {
        totalDiscards++;
        if(totalDiscards % 2 != 0) return;
        drawPosibilities++;
        OnCardRequested.Raise(drawPosibilities);

    }

    public void PlayerDiscard(CardInstance c)
    {
        if(c.isQueued)
        {
            OnError?.Raise($"Can't discard, {c.CardEffect.CardName} has been queued to play");
            return;
        }

        CheckForDrawUpdate();
        DiscardCard(c);
    }

    public void DiscardCard(CardInstance c)
    {
        // Debug.Log(hand);
        if(c.isQueued)
        {
            OnError?.Raise($"Can't discard, {c.CardEffect.CardName} has been queued to play");
            return;
        }
        this.hand.RemoveCard(c.CardEffect);
        discardPile.Add(c.CardEffect);
        Destroy(c.transform.parent.gameObject);
        OnDiscard.Raise();
    }

    public void PlayCard(EnemyInstance target)
    {

    }

}