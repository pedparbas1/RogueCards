using UnityEngine;

public abstract class TargetEntity : MonoBehaviour 
{
    
    public abstract void TakeDamage(int dmg, EffectType type);
    public abstract void  AddBuff(EffectType resist);

}