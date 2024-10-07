using UnityEngine;

public class VariableStarter : MonoBehaviour
{
    [SerializeField] GlobalVariable<EnemyData> globalVariable;
    
    [SerializeField] EnemyData value;
    void Start()
    {
        globalVariable.Value = value;
    //    value = globalVariable.Value; 
    }

}
