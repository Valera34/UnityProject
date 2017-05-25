using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{
   
    protected override void OnRabitHit(HeroController rabit)
    {
        //  Level.current.addCoins(1);
        
        rabit.Scale();
        this.CollectedHide();
    }
}
