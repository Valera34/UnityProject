using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        HeroController rabit = collider.GetComponent<HeroController>();

        if (rabit != null)
        {
            LevelController.current.onRabitDeath(rabit);
            HeroController.life--;
            HeroController.Health();
        }
    }
}