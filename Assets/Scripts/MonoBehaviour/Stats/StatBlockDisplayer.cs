using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatBlockDisplayer : MonoBehaviour
{
    public StatDict stats;
    [SerializeField] StatDisplayer displayerTemplate;
    
    public void SetupAbilities(StatDict stats)
    {
        foreach(Transform t in transform) Destroy(t.gameObject);
        // Debug.Log("Setting statsDisplayer");
        this.stats = stats;
        foreach (AbilitieScoreType ability in StatsCollection.Instance.GetAllAbilities())
        {
            StatDisplayer stat = Instantiate(displayerTemplate, transform);
            stat.SetStat(ability.name, stats.GetModifier(ability));
        }
    }

    
}
