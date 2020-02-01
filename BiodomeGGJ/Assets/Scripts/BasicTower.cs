using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{


    // Start is called before the first frame update
    protected override void Start()
    {
        initilize();
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();
    }

    protected override void initilize()
    {
        base.maxAmmo = 100;
        base.currentAmmo = maxAmmo;
        base.attackTimeMax = 60.0f;
        //change these color values values later, just tests
        base.colorRGB.Set(1, 1, 1);
        //DO NOT FORGET!!!^^
        base.attackTimeCurrent = attackTimeMax;

        base.damage = 10;
    }
}