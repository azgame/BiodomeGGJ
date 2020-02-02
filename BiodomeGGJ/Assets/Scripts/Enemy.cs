using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public abstract class Enemy : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject goal;
    public int maxhealth;
    public int currenthealth;
    public Vector3 MycolorRGB;
    public Slider healthslider;
    public Image healthFillColour;
    SkinnedMeshRenderer mr;
    InventoryItem inventoryType;
    // Start is called before the first frame update
    virtual protected void Start(){
        initilize();
        rb = GetComponent<Rigidbody>();
        goal = GameObject.FindGameObjectWithTag("Goal");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if(goal)
        {
         
            agent.destination = goal.transform.position;
        }
        healthslider.maxValue = maxhealth;
        healthslider.value = currenthealth;
       
    }

    // Update is called once per frame
  virtual  protected void Update()
    {
        
    }
    virtual protected void initilize()
    {

    }
    public void takeDamage(Vector3 colorRGB,int basedamage)
    {
        bool isneutral;
        if(colorRGB==new Vector3(1,1,1))
        {
            isneutral = true;
        }
        else
        {
            isneutral = false;
        }
        float reddamage = basedamage * colorRGB.x;
        if(MycolorRGB.y>0&&!isneutral)
        {
            reddamage *= MycolorRGB.y;
        }
      
        float greendamage = basedamage * colorRGB.y;
        if (MycolorRGB.z > 0&&!isneutral)
        {
            greendamage *= MycolorRGB.z;
        }
        float bluedamage = basedamage * colorRGB.z;
        if (MycolorRGB.x > 0&&!isneutral)
        {
            bluedamage *= MycolorRGB.x;
        }
        int totaldamage = Convert.ToInt32(reddamage + bluedamage + greendamage);
        currenthealth -= totaldamage;
        healthslider.value = currenthealth;
        if (currenthealth <= 0)
        {
            death();
        }   
        

    }
    public void death()
    {
        foreach(GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            tower.GetComponent<Tower>().EnemyDeath(this.gameObject);
        }
      
       
      //remove this object from all myenemy lists
        Destroy(this.gameObject);
    }
    public void SetInventoryType(InventoryItem type)
    {
        mr = GetComponentInChildren        <SkinnedMeshRenderer>();
        inventoryType = type;
        switch (type)
        {
            case InventoryItem.RED:
                mr.material = Resources.Load("Materials/RedMat", typeof(Material)) as Material;
                MycolorRGB.Set(3, 0, 0);
                healthFillColour.color = Color.red;
                break;
            case InventoryItem.GREEN:
                mr.material = Resources.Load("Materials/GreenMat", typeof(Material)) as Material;
                MycolorRGB.Set(0, 3, 0);
                healthFillColour.color = Color.green;
                break;
            case InventoryItem.BLUE:
                mr.material = Resources.Load("Materials/BlueMat", typeof(Material)) as Material;
                MycolorRGB.Set(0, 0, 3);
                healthFillColour.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
    