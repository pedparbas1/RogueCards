using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameObject instance;
    public GameEvent gameEvent;
    [SerializeField] protected UnityEvent response;

    public virtual void OnEnable()
    {
        if(gameEvent) 
            gameEvent.RegisterListener(this);
    }

    public virtual void OnDisable()
    {
        if(gameEvent)
            gameEvent.UnregisterListener(this);
    }

    public virtual void OnEventRaise()
    {
        response?.Invoke();
    }

    public void AddListener(UnityAction action)
    {
        response.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        response.RemoveListener(action);
    }

    public void ChangeGameObjectName()
    {
        if(gameEvent != null)
            gameObject.name = "On" + gameEvent.name[3..];
    }
}
