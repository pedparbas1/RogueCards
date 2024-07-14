using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName="Abilitie Score/AbilitieScoreType")]
public class AbilitieScoreType : ScriptableObject
{
    [SerializeField] string abilitieName;
    [SerializeField] Color abilitieColor;
}
