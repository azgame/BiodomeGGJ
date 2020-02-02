using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{

    List<InventoryItem> wave;

    // Start is called before the first frame update
    void Start()
    {
        wave = new List<InventoryItem>();
    }

    public void SetWave(List<InventoryItem> wave_)
    {
        wave = wave_;

        
    }
}
