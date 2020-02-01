using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    protected int  maxAmmo;
    protected int  currentAmmo;

    [SerializeField]
    protected float attackTimeMax;
    [SerializeField]
    protected float attackTimeCurrent;

    protected int damage;
    public GameObject bullet;
    public GameObject spawnLocation;
    protected List<GameObject> myEnemies;
    //float ammo;


    virtual protected void Start()
    {
        myEnemies=new List<GameObject>();
        myEnemies.Clear();
    }

    virtual protected void initilize()
    {

    }
    
    virtual protected void Update()
    {
        
         if (myEnemies.Count != 0)
         {
            this.gameObject.transform.LookAt(myEnemies[0].transform);
            attack();
         }
    }

    virtual  protected void attack()
    {
        if (attackTimeCurrent <= 0&&currentAmmo>0)
        {
            Instantiate(bullet,spawnLocation.transform);
            currentAmmo--;
            attackTimeCurrent = attackTimeMax;
        }
        else
        {
            attackTimeCurrent--;
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            Debug.Log("enter");
            myEnemies.Add(collision.gameObject);
        }
        
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && myEnemies.Contains(collision.gameObject))
        {
            Debug.Log("exit");
            myEnemies.Remove(collision.gameObject);
        }
    }

    virtual  protected void death()
    {
        //die and turn broken etc
    }
}
