using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Collectable
{

    protected override void OnRabitHit(HeroController rabit)
    {
        //  Level.current.addCoins(1);

        rabit.Explosion();
        this.CollectedHide();
    }
}