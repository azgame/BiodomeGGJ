﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicYellowEnemy : Enemy
{// Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void initilize()
    {
        MycolorRGB.Set(2, 2, 0);
        maxhealth = 100;
        currenthealth = maxhealth;
    }
}