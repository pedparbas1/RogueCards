using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public abstract class CardEffect : ScriptableObject
{
    [SerializeField] string cardName; 
    public string CardName {get => cardName; private set => cardName = value;}

    [SerializeField] int staminaCost; 
    public int StaminaCost {get => staminaCost; private set => staminaCost = value;}

    [SerializeField] Sprite weapon;
    public Sprite Weapon {get => weapon; private set => weapon = value;}

    [SerializeField] Dice dice;
    public Dice Dice {get => dice;}

    [SerializeField] bool isExplosive = false;
    public bool IsExplosive {get => isExplosive; private set => isExplosive = value;}
    [SerializeField] AbilitieScoreType abilityScore;
    public AbilitieScoreType AbilityScore {get => abilityScore;}

    public abstract Task<bool> PlayEffectAsync(CardPlayerHandler context);

    internal void Upgrade(CardUpgrade cardUpgrade)
    {
        throw new NotImplementedException();
    }

    public CardInstance cardVisual; 

    [ContextMenu("Update CardName")]
    public void UpdateCardName()
    {
        this.name = cardName;
        AssetDatabase.RenameAsset(AssetDatabase.FindAssets(this.name)[0], CardName);
    }
}
