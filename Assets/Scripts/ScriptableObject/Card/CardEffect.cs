using Unity;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public abstract void Apply(ITargetEntity source, ITargetEntity target);
}
