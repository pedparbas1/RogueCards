using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTurnHandler : MonoBehaviour
{
    [SerializeField] EnemyInstanceVariable heroTargetVariable;
    [SerializeField] EnemyInstance hero;

    [SerializeField] StringEvent OnError;

    [SerializeField] DamageType defaultIfMissingDamageType;
    
    void Start()
    {
        hero = heroTargetVariable != null ? heroTargetVariable.Value : null;
        
    }

    public void Handle(List<EnemyInstance> enemies)
    {
        List<Task> attacks = new();
        foreach(EnemyInstance enemy in enemies)
        {
            StartCoroutine(HandleSingle(enemy, 1));
        }
        Debug.Log("Waiting...");
    }

    private IEnumerator HandleSingle(EnemyInstance enemy, float secondsDelay)
    {
        if(enemy == null) yield break;
        if(hero == null) 
        {
            OnError?.Raise("Missing Hero");
            yield break;
        }
        
        int damage = enemy.Character.Stats.GetDamageByAbilityMod();
        Debug.Log($"Damaging {damage}");
        DamageType damageType = enemy.Character.Stats.attackDamageType;
        yield return new WaitForSeconds(1f + secondsDelay);
        hero.Character.TakeDamage(damage, damageType);
        yield return new WaitForSeconds(2f);
        yield return null;
    }
    
}