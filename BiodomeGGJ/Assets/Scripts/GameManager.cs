using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    Queue<List<InventoryItem>> m_enemyWaveQ;

    // Base health
    GameObject mainbase;
    public GameObject basicEnemy;
    public int spawnIndex;
    public int basehealth;

    // Wave timer
    int roundtimer;
    int roundtimermax;
    int spawntimemax;
    int spawntime;
    int roundbreaklength;
    int roundbreaklengthmax;
    int numRounds;

    [SerializeField]
    bool onbreak;

    public List<Spawner> m_rSpawners;
    public List<Spawner> m_eSpawners;
    public GameObject resource;
    public GameObject enemy;

    // UI
    public Canvas ui_CardQUI;
    public GameObject ui_Card;

    // Start is called before the first frame update
    void Start()
    {
        numRounds = 10;
        spawntimemax = 300;
        roundtimermax = 3600;
        roundtimer = 300;
        roundbreaklengthmax = 1800;
        roundbreaklength = roundbreaklengthmax;
        onbreak = true;
        spawntime = -1;

        Queue<InventoryItem> spawnObjects = new Queue<InventoryItem>();
        
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
            s.AddObjects(resource, new Queue<InventoryItem>());
        }

        //foreach (Spawner s in m_eSpawners)
        //{
        //    s.AddObjects(enemy, spawnObjects);
        //}

        m_enemyWaveQ = new Queue<List<InventoryItem>>();
        CreateQueue(8);
        CreateQueue(8);
        CreateQueue(8);
    }

    // Update is called once per frame
    void Update()
    {
        if (roundtimer > 0 && roundbreaklength <= 0 && onbreak == false)
        {
            if (spawntime < 0)
            {
                SpawnEnemy();
                CreateQueue(8);
                spawntime = spawntimemax;
            }
            spawntime--;
            roundtimer--;
        }
        else if (roundtimer <= 0 && roundbreaklength <= 0 && onbreak == false)
        {
            roundbreaklength = roundbreaklengthmax;
            onbreak = true;
            Debug.Log("round finished");
            numRounds--;
        }
        else if (onbreak == true && roundbreaklength > 0)
        {
            roundbreaklength--;
        }
        else if (onbreak == true && roundbreaklength <= 0)
        {
            //new round
            onbreak = false;
            roundtimer = roundtimermax;
            Debug.Log("new round coming");
        }
        else
        {
            Debug.Log("Some logic error");
        }

        if (numRounds == 0)
            Debug.Log("You win");
    }

    public void BaseDamage(int damage)
    { 
        basehealth -= damage;
        if(basehealth < 0)
        {
            //close game or go back to main menu
            Debug.Log("gameover");
        }
    }

    void CreateQueue(int totalunits)
    {
        List<InventoryItem> enemyList = new List<InventoryItem>();
        for (int i = 0; i < totalunits; i++)
        {
            int rInt = Random.Range(0, 3);
            switch (rInt)
            {
            case 0:
                enemyList.Add(InventoryItem.BLUE);
                break;
            case 1:
                enemyList.Add(InventoryItem.RED);
                break;
            case 2:
                enemyList.Add(InventoryItem.GREEN);
                break;
            default:
                break;
            }
        }

        m_enemyWaveQ.Enqueue(enemyList);
    }

    void SpawnEnemy()
    {
        if (m_enemyWaveQ.Count > 0)
        {
            m_eSpawners[spawnIndex].AddObjects(enemy, m_enemyWaveQ.Dequeue());
        }

        NextItem();
    }

    // Spawn location utitlies
    void NextItem()
    {
        spawnIndex++;
        spawnIndex %= m_rSpawners.Count;
    }

    void PreviousItem()
    {
        spawnIndex--;
        if (spawnIndex < 0)
        {
            spawnIndex = m_rSpawners.Count - 1;
        }
    }
}
