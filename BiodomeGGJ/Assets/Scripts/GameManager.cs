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
    Resources resource;

    // Start is called before the first frame update
    void Start()
    {
        m_spawners = new List<Spawner>();
        Spawner[] spawns = FindObjectsOfType<Spawner>();
        foreach (Spawner s in spawns)
            m_spawners.Add(s);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
