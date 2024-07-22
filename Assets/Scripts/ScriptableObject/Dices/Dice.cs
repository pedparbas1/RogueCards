using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
[CreateAssetMenu(fileName = "dX", menuName = "General/ Dices")]
[Serializable]
public class Dice : ScriptableObject, IDice 
{
    [SerializeField] public GameObject dicePF;
    public string DieName { get; private set;}
    [SerializeField] protected int MAX;
    private int max;

    public int ThrowDie()
    {
        Debug.Log(DieName);
        int res =  Random.Range(1, max);
        return res;
    }


    private void OnEnable()
    {
        max = MAX + 1;
        DieName = "d" + MAX;
    }

}
public interface IDice
{
    public int ThrowDie();
}
