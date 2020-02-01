using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Enemy queue
    // Enemy spawn locations -> gameobject for spawner
    // Base health
    // Wave timer
    // Game timer

    public List<Spawner> m_rSpawners;
    public List<Spawner> m_eSpawners;
    public GameObject resource;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }
}
