using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{


    // Start is called before the first frame update
    protected override void Start()
    {
        initialize();
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();
    }

    protected override void initialize()
    {
        base.maxAmmo = 100;
        base.currentAmmo = maxAmmo;
        base.attackTimeMax = 60.0f;
        base.attackTimeCurrent = attackTimeMax;

        base.damage = 10;
        base.initialize();
    }
}