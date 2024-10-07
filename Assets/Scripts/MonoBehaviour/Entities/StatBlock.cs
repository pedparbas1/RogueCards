using System;
using UnityEngine;

[Serializable]
public class StatBlock 
{
    public int baseHP;
    public int baseDamage = 0;
    public DamageType attackDamageType;
    public AbilitieScoreType damagingAbilitie;
    [Range(1, 20)] public int level = 1;

    [SerializeField] StatDict _stats;

    [SerializeField] DamageType[] _immunities;
    [SerializeField] DamageType[] _resistances;
    [SerializeField] DamageType[] _weakneses;

    public StatDict Stats
    {
        get => _stats;
        private set => _stats = value;
    }

    public DamageType[] Immunities
    {
        get => _immunities;
        private set => _immunities = value;
    }

    public DamageType[] Resistances 
    {
        get => _resistances;
        private set => _resistances = value;
    }

    public DamageType[] Weakneses
    {
        get => _weakneses;
        private set => _weakneses = value;
    }

    public StatBlock(StatBlock sb) :
    this(sb.baseHP, sb.baseDamage, sb.attackDamageType, sb.level, sb.Stats, sb.Resistances, sb.Weakneses, sb.Immunities){}

    public StatBlock(int baseHP, int baseDamage,DamageType attackDamageType, int level, StatDict stats,
    DamageType[] resistances, DamageType[] weakneses, DamageType[] immunities)
    {
        this.baseHP = baseHP;
        this.baseDamage = baseDamage;
        this.attackDamageType = attackDamageType;
        this.level = level;
        Stats = stats;
        Resistances = resistances;
        Weakneses = weakneses;
        Immunities = immunities;
    }

    public int GetDamageByAbilityMod() => GetDamageByAbilityMod(damagingAbilitie);
    public int GetDamageByAbilityMod(AbilitieScoreType abilitieScoreType)
    {
        int mod;
        if (abilitieScoreType == null) 
        {
            mod = Stats.GetModifier(abilitieScoreType);
        } else {
            mod = Stats.GetModifier(damagingAbilitie);
        }
        return (int) Math.Max(Math.Ceiling((decimal) baseDamage * level + mod), 0);
    }
}
