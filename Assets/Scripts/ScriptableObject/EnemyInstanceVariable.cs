using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="new EnemyInstanceVariable", menuName ="Variables/new EnemyInstanceVariable")]
public class EnemyInstanceVariable : GlobalVariable<EnemyInstance>
{
    public void SetValue(EnemyInstance enemy)
    {
        this.Value = enemy;
    }
    
}