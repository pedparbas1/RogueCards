using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericGameEvent<T> : ScriptableObject  
{
    private List<GenericGameEventListener<T>> listeners = new();

    public void Raise(T eventData)
    {
        Debug.Log(eventData.ToString());
        foreach(GenericGameEventListener<T> gl in listeners)
        {
            gl.OnEventRaise(eventData);
        }
    }

    public void RegisterListener(GenericGameEventListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GenericGameEventListener<T> listener)
    {
        listeners.Remove(listener);
    }
}