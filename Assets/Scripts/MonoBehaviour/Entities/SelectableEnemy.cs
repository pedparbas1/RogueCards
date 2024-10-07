using UnityEngine;

public class SelectableEnemy : Selectable, IDamageable
{
    public StatBlock _enemyStats;

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    void Awake()
    {

    }

    public float CalculateDamage(CardEffect cardEffect)
    {
        throw new System.NotImplementedException();
    }

    public void PreviewDamage()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public float CalculateDamage(float damage, DamageType damageType)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        throw new System.NotImplementedException();
    }

    public float CalculateDamageModifier(DamageType damageType)
    {
        throw new System.NotImplementedException();
    }
}
