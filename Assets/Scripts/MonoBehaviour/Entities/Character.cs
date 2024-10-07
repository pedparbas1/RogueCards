using System;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Character : IDamageable
{
    public EnemyData enemyData;
    public StatBlock Stats;
    
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int shield = 0;

    public UnityAction OnDeath;
    public UnityAction<int> OnHealthModified;
    public UnityAction OnEffectApplied;
    public UnityAction<string> Notify;

    public void Initialize()
    {
        Stats = new(enemyData.statBlockInstance.Stats);

        AbilitieScoreType constitution = StatsCollection.Instance.Abilities["con"];
        // Debug.Log(constitution);
        maxHealth = currentHealth = Stats.baseHP + Stats.Stats.GetModifier(constitution) * Stats.level;
    }

    public float CalculateDamageModifier(DamageType damageType)
    {
        if(Stats.Immunities.Contains(damageType))
        {
            Notify?.Invoke("Immune!");
            return 0f;
        }
        if(Stats.Resistances.Contains(damageType))
        {
            Notify?.Invoke("Resisted!");
            return .5f;
        }

        if (Stats.Weakneses.Contains(damageType))
        {
            Notify?.Invoke("Vulnerable!");
            return 2f;
        }

        return 1;
    }

    public void Heal(int value)
    {
        currentHealth = Math.Min(currentHealth + value, maxHealth);

        OnHealthModified.Invoke(currentHealth);
        Notify?.Invoke($"+{value}");
    }

    public void AddShield(int value) 
    {
        shield += value;
        Notify.Invoke($"Shielded: {shield}");

    }

    public void PreviewDamage()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        float totalDamage = 0;
        if(shield >= damage)
        {
            shield -= (int)damage; 
            totalDamage = 0;
        } else {
            totalDamage = damage - shield;
        }

        totalDamage = (int) Math.Ceiling(totalDamage * CalculateDamageModifier(damageType));
        currentHealth = (int) Math.Max(currentHealth - totalDamage, 0);

        Notify?.Invoke($"-{totalDamage}");
        OnHealthModified?.Invoke(currentHealth);
        if(currentHealth == 0) OnDeath?.Invoke();
    }
}