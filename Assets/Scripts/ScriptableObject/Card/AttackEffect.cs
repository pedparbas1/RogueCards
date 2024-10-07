using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new AttackEffect", menuName = "Cards/Attack")]
public class AttackEffect : CardEffect 
{
    [SerializeField] EffectType effectType;
    public EffectType EffectType {get => effectType;}

    [SerializeField] DamageType damageType;
    public DamageType DamageType {get => damageType;}

    public async override Task<bool> PlayEffectAsync(CardPlayerHandler context)
    {
        EnemyInstance target = context.enemySelected.Value;
        if(target == null)
        {
            context.OnError("No enemy selected");
            return false;
        }

        int value = await context.diceThrower.ThrowDice(Dice, IsExplosive);
        target.Character.TakeDamage(value, damageType);
        
        return true;
    }
}