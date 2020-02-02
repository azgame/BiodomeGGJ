using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    GameObject m_spawnObject;
    Queue<InventoryItem> m_spawnType;
    List<GameObject> m_spawnLocations;
    GameObject lastSpawn;
    int spawnRate;
    int spawnTimer;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnType = new Queue<InventoryItem>();
        m_spawnLocations = new List<GameObject>();

        foreach (Transform child in transform)
        {
            m_spawnLocations.Add(child.gameObject);
        }

        index = 0;
        spawnTimer = 0;
        spawnRate = 400;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_spawnType.Count > 0)
        {
            if (spawnTimer < spawnRate)
                spawnTimer++;
            else
            {
                spawnTimer = 0;
                SpawnObjects();
            }
        }

        if (m_spawnType.Count <= 0)
            if (m_spawnObject != null)
                if (m_spawnObject.tag == "Resource")
                    GenerateSpawnQueue();
    }


    void GenerateSpawnQueue()
    {
        for (int i = 0; i < 25; i++)
        {
            int rInt = Random.Range(0, 3);
            switch (rInt)
            {
            case 0:
                m_spawnType.Enqueue(InventoryItem.BLUE);
                break;
            case 1:
                m_spawnType.Enqueue(InventoryItem.RED);
                break;
            case 2:
                m_spawnType.Enqueue(InventoryItem.GREEN);
                break;
            default:
                break;
            }
        }
    }

    
    void SpawnObjects()
    {
        for (int i = 0; i < m_spawnLocations.Count; i++)
        {
            if (!m_spawnLocations[index].GetComponent<SpawnTrigger>().isTriggered)
            {
                if (m_spawnObject.tag == "Resource")
                {
                    if (lastSpawn != null)
                    {
                        lastSpawn.GetComponent<Resource>().SetInventoryType(ResourceStates.INUSE, lastSpawn.GetComponent<Resource>().getInventoryType());
                    }
                        
                    InventoryItem itemType = m_spawnType.Dequeue();
                    GameObject go = Instantiate(m_spawnObject, m_spawnLocations[index].transform);
                    go.GetComponent<Resource>().SetInventoryType(ResourceStates.SPAWNING, itemType);
                    lastSpawn = go;
                }
                else
                {
                    InventoryItem itemType = m_spawnType.Dequeue();
                    GameObject go = Instantiate(m_spawnObject, m_spawnLocations[index].transform);
                    go.GetComponent<Enemy>().SetInventoryType(itemType);
                }
                
                break;
            }
            NextItem();
        }
    }

    // Spawn add objects to queue utitlies
    public void AddObjects(GameObject spawnObject_, Queue<InventoryItem> spawnType_)
    {
        foreach(InventoryItem item in spawnType_)
        {
            m_spawnType.Enqueue(item);
        }

        m_spawnObject = spawnObject_;
    }

    public void AddObjects(GameObject spawnObject_, List<InventoryItem> spawnType_)
    {
        foreach (InventoryItem item in spawnType_)
        {
            m_spawnType.Enqueue(item);
        }

        m_spawnObject = spawnObject_;
    }

    public void AddObject(GameObject spawnObject_, InventoryItem spawnType_)
    {
        m_spawnType.Enqueue(spawnType_);
        m_spawnObject = spawnObject_;
    }

    // Spawn location utitlies
    void NextItem()
    {
        index++;
        index %= m_spawnLocations.Count;
    }

    void PreviousItem()
    {
        index--; 
        if (index < 0)
        {
            index = m_spawnLocations.Count - 1; 
        }
    }

}
