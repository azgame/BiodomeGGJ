using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject goal;
    public int maxhealth;
    public int currenthealth;
    public Vector3 MycolorRGB;
    
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
        float reddamage = basedamage * colorRGB.x-basedamage* (MycolorRGB.z*0.5f);
        if(reddamage<0)
        {
            reddamage = 0;
        }
        float greendamage = basedamage * colorRGB.y - basedamage * (MycolorRGB.x * 0.5f);
        if (greendamage < 0)
        {
            greendamage = 0;
        }
        float bluedamage = basedamage * colorRGB.z - basedamage * (MycolorRGB.y * 0.5f);
        if (bluedamage < 0)
        {
            bluedamage = 0;
        }
        int totaldamage = Convert.ToInt32(reddamage + bluedamage + greendamage);
        currenthealth -= totaldamage;
        if (currenthealth <= 0)
        {
            death();
        }   
        

    }
    public void death()
    {

    }
}
    