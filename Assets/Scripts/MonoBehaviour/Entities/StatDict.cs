using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[Serializable]
public class StatDict : SerializedDict<AbilitieScoreType, int>
{
    public int GetModifier(AbilitieScoreType key)
    {
        return (int) Math.Floor((decimal) (this[key]-10)/2);
    }
}