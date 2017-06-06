using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable
{
    protected override void OnRabitHit(HeroController rabit)
    {

        FruitsMenu.accept2 = true;
        //  Level.current.addCoins(1);
        this.CollectedHide();
    }
}
