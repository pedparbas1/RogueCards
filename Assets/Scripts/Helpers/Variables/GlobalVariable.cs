using UnityEngine;

public abstract class GlobalVariable<T> : ScriptableObject
{
    [SerializeField] protected T value;
    public virtual T Value { get => value; set => this.value = value; }
}
