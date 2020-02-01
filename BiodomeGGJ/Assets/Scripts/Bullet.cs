using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
   protected int deathtime;
   protected int movementSpeed;
    public Vector3 colorRGB;
    public int damage;
    // Start is called before the first frame update
   virtual protected void Start()
    {
        initilize();
    }

    // Update is called once per frame
  virtual  protected void Update()
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
            if(other.GetComponent<Enemy>())
            {
                other.GetComponent<Enemy>().takeDamage(colorRGB,damage);
            }
            Destroy(this.gameObject);
            
        }
    }
}
