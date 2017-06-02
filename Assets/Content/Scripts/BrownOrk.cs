using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrk : MonoBehaviour
{
    Rigidbody2D myBody = null;

    // Use this for initialization
    float last_carrot = 0;
    public Vector3 MoveBy;
    Vector3 rabit_pos;
    public Vector3 speed;
    Vector3 pointA;
    Vector3 pointB;
    bool going_to_a = false;
    public float time_to_wait = 2;
    bool ok = true;
    float t = 0;
    void Start()
    {
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
    public static bool attack = false;

    public static bool col = false;
    float time = 0.88f;
    void OnCollisionEnter2D(Collision2D myCollision)
    {
        Vector3 my_pos2 = this.transform.position;
        if (myCollision.gameObject.name == "Rabit")
        {
            BrownOrk ork = GetComponent<BrownOrk>();
            if (HeroController.my_pos.y - my_pos2.y > 0.9)
            {
                death = true;
            }
            else
            {
                col = true;
                attack = true;
            }

        }
    }
    public static Vector3 brown;
    bool go = false;
    // Update is called once per frame
   
    void Update()
    {

        Animator animator = GetComponent<Animator>();
        if (go)
        {
            animator.SetBool("walk", true);
            go = false;
        }
        if (!walk)
        {
            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BrownOrk-Idle") &&
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

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BrownOrk-Death") &&
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
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BrownOrk-Attack") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                animator.SetBool("attack", false);
                
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
    public GameObject prefabCarrot;
    void launchCarrot(float direction)
    {
        //Створюємо копію Prefab
        GameObject obj = GameObject.Instantiate(this.prefabCarrot);
        //Розміщуємо в просторі
        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+1,0);
        //Запускаємо в рух
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
    }
    public static float direction = -1;
    void FixedUpdate()
    {
        rabit_pos = HeroController.lastRabit.transform.position;
        Vector3 my_pos = this.transform.position;
        Vector3 target;
        brown= this.transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        if (Mathf.Abs(rabit_pos.x - my_pos.x) < 5.0f && rabit_pos.y < my_pos.y + 2 && rabit_pos.y > my_pos.y - 1)
        {
            
            //check launch time
            if (Time.time - last_carrot > 2.0f)
            {
                //fix the time of last launch
                last_carrot = Time.time;
                launchCarrot(direction);
            }
           
            mode = Mode.Attack;
        }
        else
        {
            attack = false;
            go = true;
        }

        if (mode == Mode.Attack)
        {
            attack = true;
            //Move towards rabit
            if (my_pos.x < rabit_pos.x)
            {
                direction = 1;
                sr.flipX = true;
                mode = Mode.GoToA;
            }
            else
            {
                direction = -1;
                sr.flipX = false;
                mode = Mode.GoToB;
            }

        }
        if (!death && !attack)
        {
            if (ok)
            {

                t = time_to_wait;
                ok = false;
            }

            t -= Time.deltaTime;
            if (t <= 0)
            {
               

                if (mode == Mode.GoToA)
                {
                    direction = 1;
                    mode = Mode.GoToA;
                    target = this.pointA;
                    sr.flipX = true;
                    this.transform.position += speed * Time.deltaTime;
                }
                else
                {
                    direction = -1;
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
