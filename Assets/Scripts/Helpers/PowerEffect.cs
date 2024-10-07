using System;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName ="new PowerEffect", menuName ="CardEffects/PowerEffect")]
public class PowerEffect : CardEffect
{
    public override Task<bool> PlayEffectAsync(CardPlayerHandler context)
    {
        throw new NotImplementedException();
    }

    [ContextMenu("UpdateName")]
    public void UpdateName() => name = $"{this.CardName} - PowerEffect";
}