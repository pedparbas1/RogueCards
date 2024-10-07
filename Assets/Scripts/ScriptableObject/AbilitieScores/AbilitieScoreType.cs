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
