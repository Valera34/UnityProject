using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    public Vector3 speed;
    Vector3 pointA;
    Vector3 pointB;
    bool going_to_a = false;
   public float time_to_wait  = 2;
    bool ok = true;
    float t = 0;
    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
    }
    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.5f;
    }
   
    // Update is called once per frame
    void Update () {
        Vector3 my_pos = this.transform.position;
        Vector3 target;
        
        if (ok)
        {
            t = time_to_wait;
            ok = false;
        }
        t -= Time.deltaTime;
        if (t <= 0)
        {
           

            if (going_to_a)
            {
                target = this.pointA;
                
                this.transform.position += speed * Time.deltaTime;
            }
            else
            {
                target = this.pointB;
                
                this.transform.position -=  speed * Time.deltaTime;
            }

            if (isArrived(my_pos, target))
            {
                going_to_a =!going_to_a;
                t = time_to_wait;
            }
            


            Vector3 destination = target - my_pos;
            destination.z = 0;
        }
    }
}
