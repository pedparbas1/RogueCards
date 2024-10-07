using System;
using Debug = UnityEngine.Debug;

[Serializable]
public class Deck : SerializedDict<CardEffect, int> 
{
    public int Size
    {
        get
        {
            int res = 0;
            if(Dict == null) return res;
            for(int i = 0; i < Dict.Length; i++)
            {
                res += Dict[i].Value;
            }
            return res;
        }
    }
    private readonly Random randomGen = new();

    public void Add(CardEffect cardEffect) => this.Add(cardEffect, 1);

    public new void Add(CardEffect cardEffect, int num)
    {
        if(ContainsKey(cardEffect))
        {
            this[cardEffect] += num;
            return;
        }

        base.Add(cardEffect, num);
        // Debug.Log(cardEffect.name);
    }

    public void Add(KeyValue<CardEffect, int> kvp)
    {
        Add(kvp.Key, kvp.Value);
    }
    
    public void Add(Deck deck)
    {
        foreach(KeyValue<CardEffect, int> kv in deck.Dict)
        {
            Add(kv.Key, kv.Value);
        }
    }

    public void RemoveCard(CardEffect c)
    {
        int value = 0;
        if(GetIndex(c, out int i))
        {
            value = Dict[i].Value -= 1;
        }
        if(value == 0) Remove(c);
    }

    public CardEffect RetrieveCard(CardEffect c)
    {
        RemoveCard(c);
        return c; 
    }

    public CardEffect RetrieveRandomCard()
    {
        int index = randomGen.Next(Size);
        // Debug.Log("Retrieving random: " + index);
        int cardNum = 0;
        foreach(KeyValue<CardEffect, int> kvp in Dict)
        {
            cardNum += kvp.Value;
            if(cardNum >= index)
            {
                return RetrieveCard(kvp.Key);
            }
        }
        return null;
    }

    public Deck() 
    {
        Dict = new KeyValue<CardEffect, int>[0];
    }
    
    public Deck(Deck deck)
    {
        KeyValue<CardEffect, int>[] dict = deck.Dict;
        Dict = dict;
    }

    public override string ToString()
    {
        string res = "";
        foreach(KeyValue<CardEffect, int> kv in Dict)
        {
            res += $"{kv} \n";
        }
        return res;
    }
}