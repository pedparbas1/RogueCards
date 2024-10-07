using UnityEngine;
using UnityEngine.Events;

public class GenericGameEventListener<T> : MonoBehaviour
{
    public GenericGameEvent<T> gameEvent;
    [SerializeField] protected UnityEvent<T> response;

    public void OnEventRaise(T eventData)
    {
        response?.Invoke(eventData);
    }

    void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    
    void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
}