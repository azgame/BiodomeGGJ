using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int deathtime;
    int movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        deathtime = 180;
        movementSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
        deathtime--;
        if(deathtime<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
