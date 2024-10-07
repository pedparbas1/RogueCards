using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="new ShieldingEffect", menuName ="CardEffects/ShieldingEffect")]
public class ShieldingSkill : SkillEffect
{
    public async override Task<bool> PlayEffectAsync(CardPlayerHandler context)
    {
        EnemyInstance target = context.heroPlaying.Value;
        if(target == null)
        {
            context.OnError("Missing hero to shield!");
            return false;
        }

        int value = await context.diceThrower.ThrowDice(Dice, IsExplosive);
        target.Character.AddShield(value);

        return true;
    }

    [ContextMenu("Update Name")]
    public void UpdateName() => name = $"{this.CardName} - SkillEffect: Shielding";
}
