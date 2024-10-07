using System;
using UnityEngine;
using Random = UnityEngine.Random;
[CreateAssetMenu(fileName = "dX", menuName = "General/ Dices")]
[Serializable]
public class Dice : ScriptableObject, IDice 
{
    [SerializeField] public GameObject dicePF;
    public string DieName { get; private set;}
    [SerializeField] protected int MAX;
    public int Max { get; private set;}

    public int ThrowDie()
    {
        int res =  Random.Range(1, Max);
        Debug.Log($"{name}: {res}");
        return res;
    }

    private void OnEnable()
    {
        Max = MAX + 1;
        DieName = "d" + MAX;
    }
}

public interface IDice
{
    public int ThrowDie();
}
