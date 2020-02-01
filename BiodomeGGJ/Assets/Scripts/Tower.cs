using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    // Start is called before the first frame update
  protected int  maxammo;
  protected int  currentammo;
    [SerializeField]
    protected float attacktimemax;
    [SerializeField]
    protected float attacktimecurrent;
    protected int damage;
    public GameObject bullet;
    public GameObject spawnlocation;
    protected List<GameObject> myenemies;
    //float ammo;




   virtual  protected void Start()
    {
        myenemies=new List<GameObject>();
        myenemies.Clear();

    }
   virtual  protected void initilize()
    {

    }
    
   virtual  protected void Update()
    {
        
            if (myenemies.Count != 0)
            {
                this.gameObject.transform.LookAt(myenemies[0].transform);
                attack();
            }
        
    }
   virtual  protected void attack()
    {
        if (attacktimecurrent <= 0&&currentammo>0)
        {
            Instantiate(bullet,spawnlocation.transform);
            currentammo--;
            attacktimecurrent = attacktimemax;
        }
        else
        {
            attacktimecurrent--;
        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag=="Enemy")
        {
            Debug.Log("enter");
          myenemies.Add(collision.gameObject);
            
        }
        
   }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy"&&myenemies.Contains(collision.gameObject))
        {
            Debug.Log("exit");
            myenemies.Remove(collision.gameObject);
        }
    }
  
   virtual  protected void death()
    {
        //die and turn broken etc
    }
}
