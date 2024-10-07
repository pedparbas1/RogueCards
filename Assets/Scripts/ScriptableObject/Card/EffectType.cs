using System;
using UnityEngine;
[Serializable]
public abstract class EffectType : ScriptableObject 
{
    [field: SerializeField] public string shortName { get; private set;}
    [field: SerializeField] public string effectName { get; private set;}
}
