using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new GameEvent", menuName = "General/GameEvent")]
public class GameEvent : ScriptableObject
{
    protected List<GameEventListener>  listeners = new();
    
    public virtual void Raise()
    {
        foreach(GameEventListener gl in listeners)
        {
            gl.OnEventRaise();
        }
    }
    
    public void RegisterListener(GameEventListener gl)
    {
        listeners.Add(gl);
    }

    public void UnregisterListener(GameEventListener gl)
    {
        listeners.Remove(gl);
    }
    
}
