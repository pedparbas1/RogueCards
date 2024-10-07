using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public List<EnemyInstance> enemies;
    public UnityEvent<bool> OnHandlingEndOfRound;
    public IntGameEvent OnRoundAdvance;
    public int round;

    void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        } else { 
            Destroy(gameObject);
        }
        
        round++;
        OnRoundAdvance.Raise(round);
    }   
    
    bool handlingRound = false;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private EnemyTurnHandler enemyHandler;

    public void NextRound() 
    {
        StartCoroutine(NextRoundAsync());
        Debug.Log("NextRound");
    }

    private IEnumerator NextRoundAsync()
    {
        handlingRound = true;
        //Deactivations while handling round
        OnHandlingEndOfRound.Invoke(!handlingRound);

        List<EnemyInstance> enemies = enemySpawner.GetAliveEnemies();
        enemyHandler.Handle(enemies);
        yield return new WaitForSeconds(5f);
        round++;
        OnRoundAdvance.Raise(round);

        handlingRound = false;
        //Setting active everything dynamicly
        OnHandlingEndOfRound.Invoke(!handlingRound);
    }

   
}
