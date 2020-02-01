using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    protected int  maxAmmo;
    protected int  currentAmmo;
    public Vector3 colorRGB;
    [SerializeField]
    protected float attackTimeMax;
    [SerializeField]
    protected float attackTimeCurrent;
    public Slider ammoslider;
    protected int damage;
    public GameObject bullet;
    public GameObject spawnLocation;
    protected List<GameObject> myEnemies;
    //float ammo;


    virtual protected void Start()
    {
        myEnemies=new List<GameObject>();
        myEnemies.Clear();
        if(ammoslider)
        {
            ammoslider.maxValue = maxAmmo;
            ammoslider.value = currentAmmo;
        }
    }

    virtual protected void initilize()
    {

    }
    
    virtual protected void Update()
    {
        
         if (myEnemies.Count != 0)
         {
            this.gameObject.transform.LookAt(myEnemies[0].transform);
            transform.LookAt(new Vector3(myEnemies[0].transform.position.x, transform.position.y, myEnemies[0].transform.position.z));
            attack();
         }
    }

    virtual  protected void attack()
    {
        if (attackTimeCurrent <= 0&&currentAmmo>0)
        {
            Instantiate(bullet,spawnLocation.transform.position,spawnLocation.transform.rotation);
            bullet.GetComponent<Bullet>().colorRGB.Set(colorRGB.x,colorRGB.y,colorRGB.z);
            bullet.GetComponent<Bullet>().damage = damage;
            currentAmmo--;
            ammoslider.value = currentAmmo;

            attackTimeCurrent = attackTimeMax;
            if(currentAmmo<=0)
            {
                death();
            }
        }
         if (attackTimeCurrent>=0)
        {

            attackTimeCurrent--;
        }

        
    }

   
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
           
            myEnemies.Add(other.gameObject);
        }
    }

    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && myEnemies.Contains(other.gameObject))
        {
            
            myEnemies.Remove(other.gameObject);
        }
    }

    virtual  protected void death()
    {
        //die and turn broken etc
    }
    public void chnagecolor(Vector3 newcolor)
    {
        colorRGB.Set(newcolor.x, newcolor.y, newcolor.z);
    }
}
