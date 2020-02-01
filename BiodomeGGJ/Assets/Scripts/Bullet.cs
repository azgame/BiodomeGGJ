using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
   protected int deathtime;
   protected int movementSpeed;
    public Vector3 colorRGB;
    // Start is called before the first frame update
    protected void Start()
    {
        initilize();
    }

    // Update is called once per frame
   protected void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        deathtime--;
        if(deathtime<=0)
        {
            Destroy(this.gameObject);
        }
    }
    virtual protected void initilize()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            
        }
    }
}
