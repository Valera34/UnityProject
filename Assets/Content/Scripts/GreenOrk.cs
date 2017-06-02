using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrk : MonoBehaviour {
    Rigidbody2D myBody = null;
   
    // Use this for initialization
    public Vector3 MoveBy;
    Vector3 rabit_pos;
    public Vector3 speed;
    Vector3 pointA;
    Vector3 pointB;
    bool going_to_a = false;
    public float time_to_wait = 2;
    bool ok = true;
    float t = 0;
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
      
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
    }
    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }
    bool death = false;
    bool walk = false;
    bool attack = false;

    public static bool col=false;
    float time = 0.88f;
    void OnCollisionEnter2D(Collision2D myCollision)
    {
        Vector3 my_pos2 = this.transform.position;
        if (myCollision.gameObject.name == "Rabit")
        {
            GreenOrk ork = GetComponent<GreenOrk>();
            if (HeroController.my_pos.y - my_pos2.y>0.9)
            {
                death = true;
            }
            else{
                col = true;
                attack= true;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        Animator animator = GetComponent<Animator>();

        animator.SetBool("walk", true);
        if (!walk)
        {
            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ork-Idle") &&
 animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= time_to_wait)

            {
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
                walk = true;
            }
            }
        
        if (death)
        {
            animator.SetBool("walk", false);
            animator.SetBool("die", true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ork-Die") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            
                {

                Destroy(this.gameObject);
                animator.SetBool("die", false);
                
            }
        }
        if (attack)
        {
            animator.SetBool("walk", false);
            animator.SetBool("idle", false);
            animator.SetBool("attack", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ork-Attack") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                animator.SetBool("attack", false);
                attack = false;
                walk = true;
            }
        }
    }
    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.5f;
    }
    Mode mode = Mode.GoToB;
    void FixedUpdate()
    {
        rabit_pos = HeroController.lastRabit.transform.position;
        Vector3 my_pos = this.transform.position;
        Vector3 target;

        if (!death&&!attack) { 
        if (ok)
        {
              
                t = time_to_wait;
            ok = false;
        }
            
            t -= Time.deltaTime;
            if (t <= 0)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
                && rabit_pos.x < Mathf.Max(pointA.x, pointB.x)&&rabit_pos.y < my_pos.y+2&& rabit_pos.y > my_pos.y - 1)
                {
                    mode = Mode.Attack;
                }

                if (mode == Mode.Attack)
                {
                    //Move towards rabit
                    if (my_pos.x < rabit_pos.x)
                    {
                        mode = Mode.GoToA;
                    }
                    else
                    {
                        mode = Mode.GoToB;
                    }

                }
                
                if (mode == Mode.GoToA)
                {
                    mode = Mode.GoToA;
                    target = this.pointA;
                    sr.flipX = true;
                    this.transform.position += speed * Time.deltaTime;
                }
                else
                {
                   
                    target = this.pointB;
                    sr.flipX = false;
                    this.transform.position -= speed * Time.deltaTime;
                    mode = Mode.GoToB;
                }

                if (isArrived(my_pos, target))
                {
                    walk = false;
                    going_to_a = !going_to_a;
                    if (mode == Mode.GoToB)
                    {
                        mode = Mode.GoToA;
                    }
                    else
                    {
                        mode = Mode.GoToB;
                    }
                    t = time_to_wait;
                }



                Vector3 destination = target - my_pos;
                destination.z = 0;
            }
        }
    }
    }
