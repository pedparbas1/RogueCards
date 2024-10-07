using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] EnemyDataVariable hero;
    [SerializeField] EnemyInstanceVariable spawnedHeroInstance;
    [SerializeField] EnemyInstance heroInstance;
    [SerializeField] GameObject parent;

    void Awake()
    {
        spawnedHeroInstance.Value = Spawn();
        // if(hero.Value != null) Debug.Log(hero.Value.enemyName);
    }

    public EnemyInstance Spawn()
    {
        if(hero?.Value == null)
        {
            Debug.Log("No hero selected");
            return null;
        }
        
        heroInstance.SetupInstance(hero.Value);
        
        return heroInstance;
    }
}