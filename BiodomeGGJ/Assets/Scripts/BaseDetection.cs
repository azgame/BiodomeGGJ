using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDetection : MonoBehaviour
{
    public GameObject Gmanager;
    private void Start()
    {
        Gmanager = GameObject.Find("GameManager");
        
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.GetComponent<Enemy>())
            {
                other.GetComponent<Enemy>().takeDamage(new Vector3(1,1,1), 9999);
            }
            Gmanager.GetComponent<GameManager>().BaseDamage(1);

        }
    }
}
