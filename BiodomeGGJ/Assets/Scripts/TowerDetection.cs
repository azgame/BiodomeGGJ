using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enter" + other.gameObject.GetInstanceID());
            if (!this.gameObject.GetComponentInParent<Tower>().myEnemies.Contains(other.gameObject)) {
                this.gameObject.GetComponentInParent<Tower>().myEnemies.Add(other.gameObject);
            }
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && this.gameObject.GetComponentInParent<Tower>().myEnemies.Contains(other.gameObject))
        {
            Debug.Log("Exit" + other.gameObject.GetInstanceID());
            this.gameObject.GetComponentInParent<Tower>().myEnemies.Remove(other.gameObject);
        }
    }
}
