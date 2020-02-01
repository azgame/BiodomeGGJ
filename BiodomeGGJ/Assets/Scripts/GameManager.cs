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

    List<Spawner> m_spawners;
    public GameObject resource;

    // Start is called before the first frame update
    void Start()
    {
        m_spawners = new List<Spawner>();
        
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
                
        Spawner[] spawns = FindObjectsOfType<Spawner>();
        foreach (Spawner s in spawns)
        {
            s.AddObjects(resource, spawnObjects);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
