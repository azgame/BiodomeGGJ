using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{


    // Enemy queue
    
    Queue<InventoryItem> enemyqueue;
    // Enemy spawn locations -> gameobject for spawner
    // Base health
    GameObject mainbase;
    public GameObject basicEnemy;
    public GameObject currentEnemyspawner;
    public GameObject Enemyspawner1;
    public GameObject Enemyspawner2;
    public GameObject Enemyspawner3;
    public int basehealth;
    // Wave timer
    int wavetimer;
    int wavetimermax;
    int spawntimemax;
    int spawntime;
    int wavebreaklength;
    int wavebreaklengthmax;
    [SerializeField]
    bool onbreak;

    // Game timer



    public List<Spawner> m_rSpawners;
    public List<Spawner> m_eSpawners;
    public GameObject resource;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        spawntimemax = 60;
        wavetimermax = 3600;
        wavetimer = 60;
        wavebreaklengthmax = 1800;
        wavebreaklength = wavebreaklengthmax;
        Queue<InventoryItem> spawnObjects = new Queue<InventoryItem>();
         enemyqueue = new Queue<InventoryItem>();

        foreach (InventoryItem type in Enum.GetValues(typeof(InventoryItem)))
        {
            if (type != InventoryItem.TOWER)
            {
                for (int j = 0; j < 4; j++)
                {
                    spawnObjects.Enqueue(type);
                }
            }
        }
                
        foreach (Spawner s in m_rSpawners)
        {
            s.AddObjects(resource, spawnObjects);
        }

        foreach (Spawner s in m_eSpawners)
        {
            s.AddObjects(enemy, spawnObjects);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wavetimer > 0 && wavebreaklength <= 0&&onbreak==false)
        {
            if (spawntime < 0)
            {
                SpawnEnemy();
                spawntime = spawntimemax;
            }
            spawntime--;
            wavetimer--;
        }
        else if (wavetimer<0 && wavebreaklength <= 0&&onbreak==false)
        {
            wavebreaklength = wavebreaklengthmax;
            onbreak = true;
        }
        else if(onbreak==true&&wavebreaklength>0)
        {
            wavebreaklength--;
        }
        else if(onbreak==true&&wavebreaklength<=0)
        {
            //new wave
            onbreak = false;
            wavetimer = wavetimermax;
            enemyqueue.Clear();
        }

    }
    public void basedamage(int damage)
    { 
        basehealth -= damage;
        if(basehealth<0)
        {
            //close game or go back to main menu
            Debug.Log("gameover");
        }
    }
    void createqueue(int totalunits)
    {
        
        
            for (int i = 0; i < totalunits; i++)
            {
                int rInt = Random.Range(0, 3);
                switch (rInt)
                {
                    case 0:
                        enemyqueue.Enqueue(InventoryItem.BLUE);
                        break;
                    case 1:
                    enemyqueue.Enqueue(InventoryItem.RED);
                        break;
                    case 2:
                    enemyqueue.Enqueue(InventoryItem.GREEN);
                        break;
                    default:
                        break;
                }
            }
        
    }
    void SpawnEnemy()
    {
        if (enemyqueue.Count > 0)
        {
            Instantiate(basicEnemy, currentEnemyspawner.transform.position, currentEnemyspawner.transform.rotation);
            InventoryItem itemType = enemyqueue.Dequeue();
            basicEnemy.GetComponent<Enemy>().SetInventoryType(itemType);
        }
    }
    
}
