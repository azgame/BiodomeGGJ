﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Tower : MonoBehaviour, IInteractable
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
    public List<GameObject> myEnemies;
    //float ammo;

    bool isInPickupRange = false;
    bool isInRepairRange = false;
    bool isCarried = true;

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

    virtual protected void initialize()
    {
        this.makeBroken();
    }
    
    virtual protected void Update()
    {
        
         if (this.fillCount() >= 3 && myEnemies.Count != 0)
         {
            this.gameObject.transform.LookAt(myEnemies[0].transform);
            transform.LookAt(new Vector3(myEnemies[0].transform.position.x, transform.position.y, myEnemies[0].transform.position.z));
            attack();
         }
    }

    virtual  protected void attack()
    {
        if (this.fillCount() >= 3 && attackTimeCurrent <= 0 && currentAmmo > 0 )
        {
            Instantiate(bullet,spawnLocation.transform.position,spawnLocation.transform.rotation);
            bullet.GetComponent<Bullet>().colorRGB.Set(colorRGB.x,colorRGB.y,colorRGB.z);
            bullet.GetComponent<Bullet>().damage = damage;
            currentAmmo--;
            ammoslider.value = currentAmmo;

            attackTimeCurrent = attackTimeMax;
            if(currentAmmo<=0)
            {
                makeBroken();
            }
        }
         if (attackTimeCurrent>=0)
        {

            attackTimeCurrent--;
        }

        
    }

   
   

    public void EnemyDeath(GameObject deadenemy)
    {
        if(myEnemies.Contains(deadenemy))
        {
            myEnemies.Remove(deadenemy);
        }
    }

    virtual protected void onBreak()
    {
        foreach(Transform child in this.transform)
        {
            if (child.tag == "RepairedTower") {
                child.gameObject.SetActive(false);
            } else if (child.tag == "BrokenTower") {
                child.gameObject.SetActive(true);
            }
        }
    }

    virtual protected void onRepair()
    {
        foreach(Transform child in this.transform)
        {
            if (child.tag == "RepairedTower") {
                child.gameObject.SetActive(true);
            } else if (child.tag == "BrokenTower") {
                child.gameObject.SetActive(false);
            }
        }
    }

    virtual protected void makeBroken()
    {
        this.colorRGB.Set(0, 0, 0);
        this.onBreak();
    }
    public float fillCount()
    {
        return colorRGB.x + colorRGB.y + colorRGB.z;
    }
    public bool wasTriggered(IInteractable inventory)
    {
        if (inventory == null) {
            this.isInPickupRange = true;
            return true;
        } else if (inventory.getInventoryType() != InventoryItem.TOWER) {
            this.isInRepairRange = true;
            return true;
        }
        return false;
    }

    public void wasUntriggered()
    {
        this.isInPickupRange = false;
    }

    public IInteractable activated(IInteractable inventory)
    {
        if (inventory == null) {
            this.isCarried = true;
            this.makeBroken();
            return this;
        } else if (inventory.getInventoryType() != InventoryItem.TOWER && this.fillCount() < 3) {
            switch (inventory.getInventoryType()) {
                case InventoryItem.RED:
                    {
                        this.colorRGB.x += 1;
                        break;
                    }
                case InventoryItem.GREEN:
                    {
                        this.colorRGB.y += 1;
                        break;
                    }
                case InventoryItem.BLUE:
                    {
                        this.colorRGB.z += 1;
                        break;
                    }
            }
            if (this.fillCount() == 3) {
                this.onRepair();
            }
            inventory.consumed();
        }
        return null;
    }

    public bool deactivated()
    {
        this.isCarried = false;
        return false;
    }

    public InventoryItem getInventoryType()
    {
        return InventoryItem.TOWER;
    }

    public void consumed()
    {
        // Not consumed
    }

}
