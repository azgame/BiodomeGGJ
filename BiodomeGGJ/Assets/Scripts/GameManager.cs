using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    int waveIndex;

    int enemySpawnNum;

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
        spawntimemax = 2400;
        roundtimermax = spawntimemax * 4;
        roundtimer = roundtimermax;
        roundbreaklengthmax = spawntimemax * 2;
        roundbreaklength = roundbreaklengthmax;
        onbreak = true;
        spawntime = -1;
        enemySpawnNum = 24;
        waveIndex = 0;

        basehealth = 20;
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
        CreateQueue(enemySpawnNum);
        CreateQueue(enemySpawnNum);
        CreateQueue(enemySpawnNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (roundtimer > 0 && roundbreaklength <= 0 && onbreak == false)
        {
            if (spawntime < 0)
            {
                SpawnEnemy();
                CreateQueue(enemySpawnNum);
                spawntime = spawntimemax;
                waveIndex++;
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
        if (basehealth < 0)
        {
            //close game or go back to main menu
            Debug.Log("gameover");
            Application.Quit();
        }
    }

    void CreateQueue(int totalunits)
    {
        Queue<InventoryItem> blueList = new Queue<InventoryItem>();
        Queue<InventoryItem> redList = new Queue<InventoryItem>();
        Queue<InventoryItem> greenList = new Queue<InventoryItem>();
        List<InventoryItem> enemyList = new List<InventoryItem>();
        for (int i = 0; i < totalunits; i++)
        {
            int rInt = Random.Range(0, 3);
            int spawnVal = rInt + ((waveIndex + 3) % 3) * 3;

            if (spawnVal == 0 || spawnVal == 3 || spawnVal == 6)
            {
                blueList.Enqueue(InventoryItem.BLUE);
            }
            else if (spawnVal == 1 || spawnVal == 4 || spawnVal == 7)
            {
                redList.Enqueue(InventoryItem.RED);
            }
            else if (spawnVal == 2 || spawnVal == 5 || spawnVal == 8)
            {
                greenList.Enqueue(InventoryItem.GREEN);
            }
        }

        while (blueList.Count > 0)
            enemyList.Add(blueList.Dequeue());

        while (redList.Count > 0)
            enemyList.Add(redList.Dequeue());

        while (greenList.Count > 0)
            enemyList.Add(greenList.Dequeue());

        m_enemyWaveQ.Enqueue(enemyList);
        GameObject card = Instantiate(ui_Card, ui_CardQUI.transform);

        foreach (InventoryItem item in enemyList)
        {
            GameObject image = new GameObject();
            Image enemyType = image.AddComponent<Image>();

            GameObject cardSlot = Instantiate(image, card.transform);

            switch (item)
            {
                case InventoryItem.BLUE:
                    cardSlot.GetComponent<Image>().color = Color.blue;
                    break;
                case InventoryItem.RED:
                    cardSlot.GetComponent<Image>().color = Color.red;
                    break;
                case InventoryItem.GREEN:
                    cardSlot.GetComponent<Image>().color = Color.green;
                    break;
                default:
                    break;
            }
        }
    }

    void SpawnEnemy()
    {
        if (m_enemyWaveQ.Count > 0)
        {
           
            m_eSpawners[spawnIndex].AddObjects(enemy, m_enemyWaveQ.Dequeue());
            Destroy(ui_CardQUI.transform.GetChild(0).gameObject);
        }

        NextItem();
    }

    // Spawn location utitlies
    void NextItem()
    {
        spawnIndex++;
        spawnIndex %= m_eSpawners.Count;
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
