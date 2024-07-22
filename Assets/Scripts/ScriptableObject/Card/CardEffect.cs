using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New CardEffect", menuName ="CardEffects/new CardEffect")]
public class CardEffect : ScriptableObject
{
    [SerializeField] private Effect effect; 
    public Effect Effect {get => effect;}
}
[Serializable]
public struct Effect
{
    [SerializeField] string cardName; 
    public string CardName {get => cardName; private set => cardName = value;}
    [SerializeField] int staminaCost; 
    public int StaminaCost {get => staminaCost; private set => staminaCost = value;}
    [SerializeField] Sprite weapon;
    public Sprite Weapon {get => weapon; private set => weapon = value;}
    [SerializeField] Dice dice;
    public Dice Dice {get => dice;}
    [SerializeField] AbilitieScoreType abilityScore;
    public AbilitieScoreType AbilityScore {get => abilityScore;}
    [SerializeField] DamageType damageType; 
    public DamageType DamageType {get => damageType;}
}
