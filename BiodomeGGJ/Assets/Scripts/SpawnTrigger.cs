using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }

    private void OnTriggerStay(Collider other)
    {
        isTriggered = true;
    }
}
