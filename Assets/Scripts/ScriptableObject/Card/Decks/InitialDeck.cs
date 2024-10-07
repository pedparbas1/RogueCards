using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Deck", menuName = "Cards/new CardDeck")]
public class InitialDeck : ScriptableObject
{
    [Header("Runtime modifiable")]
    [SerializeField] private bool modifiable;

    [Header("Deck: Starting")]
    [SerializeField] Deck initialDeck;

    [Header("Runtime Deck")]
    [NonSerialized] public Deck runtimeDeck;

    public Deck Deck { get => runtimeDeck;}

    public CardEffect RetrieveCard()
    {
        List<CardEffect> res = new();

        foreach(KeyValue<CardEffect, int> kv in Deck.Dict)
        {
            for(int i = 1; i <= kv.Value; i++) res.Add(kv.Key);
        }

        int index = UnityEngine.Random.Range(1,res.Count);
        CardEffect cardEffect = res[index - 1];

        runtimeDeck[cardEffect] -= 1;
        return cardEffect;
    }

    void OnEnable()
    {
        runtimeDeck = new();
        runtimeDeck.Add(initialDeck);
        // Debug.Log(runtimeDeck.ToString());
    }
}