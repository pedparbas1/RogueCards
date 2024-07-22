using System;
using UnityEngine;
/// <summary>
//[CreateAssetMenu(menuName = "Card Stats/Create Base CardStats")]
/// </summary>
[Serializable]
public class CardStats 
{
    [SerializeField] CardEffect effect; 
    public CardStats(CardStats instance)
    {
        this.effect = instance.effect;
    }

}
