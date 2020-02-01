using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Enemy queue
    // Enemy spawn locations -> gameobject for spawner
    // Resource spawn locations -> gameobject for spawner
    // Base health
    // Wave timer
    // Game timer
    List<Spawner> m_spawners;
    public List<GameObject> resources;

    // Start is called before the first frame update
    void Start()
    {
        resources = new List<GameObject>();
        m_spawners = new List<Spawner>();
        

        Queue<GameObject> spawnObjects = new Queue<GameObject>();

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 4; j++)
                spawnObjects.Enqueue(resources[i]);

        Spawner[] spawns = FindObjectsOfType<Spawner>();
        foreach (Spawner s in spawns)
        {
            s.AddObjects(spawnObjects);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
