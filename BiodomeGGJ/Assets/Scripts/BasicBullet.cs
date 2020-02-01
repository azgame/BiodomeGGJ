using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    // Start is called before the first frame update
   

    
    protected override void initilize()
    {
        base.deathtime = 180;
        base.movementSpeed = 60;
    }
}
