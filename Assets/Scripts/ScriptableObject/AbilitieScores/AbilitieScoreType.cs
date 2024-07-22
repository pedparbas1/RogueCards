using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName="Abilitie Score/AbilitieScoreType")]
public class AbilitieScoreType : ScriptableObject
{
    [SerializeField] string abilityName;
    [SerializeField] Color abilitieColor;
    public override string ToString()
    {
        return abilityName;
    }
}
