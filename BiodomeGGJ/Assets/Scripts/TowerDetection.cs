using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            this.gameObject.GetComponentInParent<Tower>().myEnemies.Add(other.gameObject);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && this.gameObject.GetComponentInParent<Tower>().myEnemies.Contains(other.gameObject))
        {

            this.gameObject.GetComponentInParent<Tower>().myEnemies.Remove(other.gameObject);
        }
    }
}
