using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityCollection")]
public class StatsCollection : ScriptableObject 
{
    private static StatsCollection _instance;
    public static StatsCollection Instance
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    public SerializedDict<string, AbilitieScoreType> Abilities; 
    public List<AbilitieScoreType> GetAllAbilities() => Abilities.GetValues();

    void OnEnable()
    {
        LoadAbilitieTypes();
    }

    [ContextMenu("LoadAllAbilities")]
    void LoadAbilitieTypes()
    {
        AbilitieScoreType[] _as = Resources.LoadAll<AbilitieScoreType>("AbilitieScoreType/");

        Abilities = new();
        foreach(AbilitieScoreType s in _as)
        {
            // Debug.Log(s.ToString());
            Abilities.Add(s.ToString().Trim(), s);
        }
    }
}
