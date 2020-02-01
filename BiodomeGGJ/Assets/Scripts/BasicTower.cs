using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        initilize();
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();
    }

    protected override void initilize()
    {
        base.maxAmmo = 10;
        base.currentAmmo = maxAmmo;
        base.attackTimeMax = 60.0f;
        
        base.attackTimeCurrent = attackTimeMax;

        base.damage = 10;
    }
}