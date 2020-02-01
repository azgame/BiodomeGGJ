using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        initilize();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    protected override void initilize()
    {
        base.maxammo = 10;
        base.currentammo = maxammo;
        base.attacktimemax = 60.0f;
        
        base.attacktimecurrent = attacktimemax;

        base.damage = 10;
        


}
   
   

}

