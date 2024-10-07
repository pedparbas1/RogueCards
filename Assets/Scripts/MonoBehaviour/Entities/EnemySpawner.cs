using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyInstance prefab;
    public GameObject enemySlot;
    [SerializeField] EnemyListVariable roomEnemies;
    [SerializeField] int maxSpawns = 4;
    [SerializeField] private GameObject[] enemySlots = new GameObject[0];

    [SerializeField] private EnemyInstance[] enemies = new EnemyInstance[4];
    [SerializeField] private GameEvent OnEnemiesKilled;

    void Awake()
    {
        Debug.Log("Starting Spawning sequence");
        if(roomEnemies == null) return;

        ClearChildren(transform);
        
        enemySlots = new GameObject[maxSpawns];
        enemies = new EnemyInstance[maxSpawns];
        roomEnemies.Value.RemoveAll(x => x == null);
    }

    void Start()
    {
        for(int j = 0; j < roomEnemies.Value.Count; j++)
        {
            Debug.Log("Enemies in room:" + roomEnemies.Value.Count);
            enemySlots[j] = Instantiate<GameObject>(enemySlot, transform);

            //Setting up enemies
            EnemyInstance enemy = Instantiate<EnemyInstance>
                                    (prefab, enemySlots[j].transform);
            enemy.SetupInstance(roomEnemies.Value[j]);
            enemy.OnDeath.AddListener(Despawn);
            enemies[j] = enemy;


            // //Setup localPosition for nice display
            enemy.GetComponent<RectTransform>().anchoredPosition = 
                j % 2 == 0 ? Vector2.up: Vector2.down;
        }

    }

    public List<EnemyInstance> GetAliveEnemies()
    {
        return enemies.Where((x) => x != null).ToList();
    }

    void Despawn(EnemyInstance target)
    {
        int slotIndex = SearchEnemyIndex(target);
        ClearChildren(target.transform.parent);
        enemies[slotIndex] = null;
        Destroy(target);
    
        if(GetAliveEnemies().Count == 0) OnEnemiesKilled.Raise();
    }

    int EmptySpots()
    {
        int res = 0;
        foreach(GameObject slot in enemySlots) res += slot.transform.childCount == 0 ? 1 : 0;
        return res;
    }

    int SearchEnemyIndex(EnemyInstance enemy)
    {
        for(int i = 0; i < maxSpawns; i++)
        {
            if(enemies[i] == enemy) return i;
        }
        return -1;
    }

    public void ClearChildren(Transform t)
    {
        foreach(Transform child in t)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Objects after clearing: " + t.childCount);
    }
}
