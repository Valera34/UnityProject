using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable
{
    public Vector3 speed;
    Vector3 rabit_pos;
    float direction = BrownOrk.direction;
    Vector3 my_pos;
    void Start()
    {
        StartCoroutine(destroyLater());
    }
    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
    protected override void OnRabitHit(HeroController rabit)
    {
        //  Level.current.addCoins(1);

        rabit.Explosion();
        this.CollectedHide();
    }
    
    void FixedUpdate()
    {
        if (direction > 0)
        {
            this.transform.position += speed * Time.deltaTime;
        }
        else
        {
            this.transform.position -= speed * Time.deltaTime;
        }
    }
   public   void launch(float direction)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (direction > 0)
        {
            sr.flipX = false;
        }
    }


}
