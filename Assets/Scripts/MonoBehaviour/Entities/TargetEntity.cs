using UnityEngine;

public abstract class TargetEntity : MonoBehaviour
{
    
    public abstract void TakeDamage(int dmg, DamageType type);
    public abstract void  AddBuff(DamageType resist);

}