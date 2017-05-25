using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : Collectable
{
    protected override void OnRabitHit(HeroController rabit)
    {
        //  Level.current.addCoins(1);
        this.CollectedHide();
    }
}
