using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    Queue<GameObject> m_spawnObjects;
    List<GameObject> m_spawnLocations;
    GameObject spawnIndex;
    int spawnRate;
    int spawnTimer;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnObjects = new Queue<GameObject>();
        m_spawnLocations = new List<GameObject>();

        foreach (Transform child in transform)
        {
            m_spawnLocations.Add(child.gameObject);
        }

        spawnTimer = 0;
        spawnRate = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnRate)
            spawnTimer++;
        else
        {
            spawnTimer = 0;
            if (m_spawnObjects.Count > 0)
                SpawnObjects();
        }
    }

    
    void SpawnObjects()
    {
        GameObject obj = new GameObject();
        for (int i = 0; i < m_spawnLocations.Count; i++)
        {
            GameObject spawnLoc = m_spawnLocations[index];
            if (!spawnLoc.GetComponent<SpawnTrigger>().isTriggered)
            {
                obj = m_spawnObjects.Dequeue();
                break;
            }

            NextItem();
        }

        if (obj != null)
        {
            Instantiate(obj, m_spawnLocations[index].transform);
        }
    }

    // Spawn add objects to queue utitlies
    public void AddObjects(Queue<GameObject> spawnObjects_)
    {
        foreach(GameObject obj in spawnObjects_)
        {
            m_spawnObjects.Enqueue(obj);
        }
    }

    public void AddObjects(List<GameObject> spawnObjects_)
    {
        foreach (GameObject obj in spawnObjects_)
        {
            m_spawnObjects.Enqueue(obj);
        }
    }

    public void AddObject(GameObject spawnObject_)
    {
        m_spawnObjects.Enqueue(spawnObject_);
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
