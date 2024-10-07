public interface IDamageable
{
    public void TakeDamage(float damage, DamageType damageType);
    public float CalculateDamageModifier(DamageType damageType);
    public void PreviewDamage();
}