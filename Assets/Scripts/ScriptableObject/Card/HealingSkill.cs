using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName ="new HealingEffect", menuName ="CardEffects/HealingEffect")]
public class HealingSkill : SkillEffect
{
    public override async Task<bool> PlayEffectAsync(CardPlayerHandler context)
    {
        EnemyInstance target = context.heroPlaying.Value;
        if(target == null)
        {
            context.OnError("Missing hero to heal!");
            return false;
        }
        
        int value = await context.diceThrower.ThrowDice(Dice, IsExplosive);
        target.Character.Heal(value);
        return true;
    }

    public void Apply(EnemyInstance target, int value)
    {

    }
}