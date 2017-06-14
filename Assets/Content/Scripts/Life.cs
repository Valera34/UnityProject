using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable
{

    protected override void OnRabitHit(HeroController rabit)
    {
        //  Level.current.addCoins(1);
        rabit.Recover();
        this.CollectedHide();
        
    }
}
