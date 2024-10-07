using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Entities/new StatBlock")]
public class StatBlockInstance : ScriptableObject
{
    [SerializeField] StatBlock _stats;
    public StatBlock Stats
    {
       get => _stats;
       private set => _stats = value; 
    }

    bool isPopulated = false;
    
    [ContextMenu("Populate")]
    public void PopulateInitial()
    {
        if (isPopulated) return;

        isPopulated = true;
        AbilitieScoreType[] abilitieScores = Resources.LoadAll<AbilitieScoreType>("AbilitieScoreType/");

        Debug.Log(abilitieScores.Length);
        foreach(AbilitieScoreType AS in abilitieScores)
        {
            // Debug.Log(AS.ToString());
            Stats.Stats.Add(AS, 10);
        }
    }
}