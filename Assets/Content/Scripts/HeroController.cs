using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeroController : MonoBehaviour {
    public float speed = 3;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    public static float life = 3f;
    Rigidbody2D myBody = null;
    // Use this for initialization
    Transform heroParent = null;
    public static HeroController lastRabit = null;
    void Awake()
    {
        l = GameObject.FindGameObjectsWithTag("Fruit").Length;
        lastRabit = this;
    }
    public static float l;
    void Start()
    {
        this.heroParent = this.transform.parent;
        LevelController.current.setStartPosition(transform.position);
        myBody = this.GetComponent<Rigidbody2D>();
    }
    bool big = false;
    public void Scale()
    {
       
        if (!big) { 
        Vector2 vel = myBody.velocity;
        vel = this.transform.localScale *= 2;
        myBody.velocity = vel;
            big = true;
        }
        
    }
    bool bang = false;
    public void Explosion()
    {

        if (big)
        {
            Vector2 vel = myBody.velocity;
            vel = this.transform.localScale /= 2;
            myBody.velocity = vel;
            big = false;
        }
        else
        {
            bang = true;
           
        }

    }
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 my_pos2 = this.transform.position;
        if (collider.gameObject.name == "Room")
        {
            SceneManager.LoadScene("Level1");

        }
        if (collider.gameObject.name == "Room (1)")
        {
            SceneManager.LoadScene("Level2");

        }
        GameObject h;
        if (collider.gameObject.name == "RedCrystal")
        {
            h = GameObject.Find("CrystalEmpty");
            Destroy(h.gameObject);
        }
        if (collider.gameObject.name == "BlueCrystal")
        {
            h = GameObject.Find("CrystalEmpty2");

            Destroy(h.gameObject);

        }
        if (collider.gameObject.name == "GreenCrystal")
        {
            h = GameObject.Find("CrystalEmpty3");
            Destroy(h.gameObject);

        }
    }
    float t = 1.1f;
    // Update is called once per frame
   
    void Update() {
        Scene scene = SceneManager.GetActiveScene();
        string s=scene.name; // name of scene
        Animator animator = GetComponent<Animator>();
        if (s=="MainScene")
        {
            animator.SetBool("idle", true);
        }
        else
        {
            float value = 0;
            if (!bang && !GreenOrk.col && !BrownOrk.col)
            {
                value = Input.GetAxis("Horizontal");
            }

            if (bang)
            {
                animator.SetBool("jump", false);
                animator.SetBool("death", true);
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    animator.SetBool("death", false);
                    HeroController rabit = GetComponent<HeroController>();
                    LevelController.current.onRabitDeath(rabit);
                    bang = false;
                    t = 1.1f;
                    life--;
                    Health();
                }
            }
            if (!bang)
            {
                if (this.isGrounded)
                {
                    animator.SetBool("jump", false);
                }
                else
                {
                    animator.SetBool("jump", true);
                }
            }
            if (Mathf.Abs(value) > 0 && this.isGrounded)
            {
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }
            Vector3 from = transform.position;
            Vector3 to = transform.position;
            int layer_id = 1 << LayerMask.NameToLayer("Ork");

            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

            if (hit)
            {



                /* animator.SetBool("jump", false);
                 animator.SetBool("death", true);
                 t -= Time.deltaTime;
                 if (t <= 0)
                 {

                     animator.SetBool("death", false);
                     HeroController rabit = GetComponent<HeroController>();
                     LevelController.current.onRabitDeath(rabit);
                     t = 1.1f;
                 }*/
            }
            if (GreenOrk.col || BrownOrk.col)
            {
                animator.SetBool("jump", false);
                animator.SetBool("death", true);
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    animator.SetBool("death", false);
                    HeroController rabit = GetComponent<HeroController>();
                    LevelController.current.onRabitDeath(rabit);
                    bang = false;
                    GreenOrk.col = false;
                    BrownOrk.col = false;
                    t = 1.1f;
                    life--;
                    Health();
                }

            }
        }
    }

    public static Vector3 my_pos;
    public static void Health()
    {
        GameObject h;
        if (life == 2)
        {
            h = GameObject.Find("Heart3");
            Destroy(h.gameObject);

        }
        if (life == 1)
        {
            h = GameObject.Find("Heart2");
            Destroy(h.gameObject);

        }
        if (life == 0)
        {
            h = GameObject.Find("Heart");
            Destroy(h.gameObject);
            SceneManager.LoadScene("ChooseLevel");
            life = 3;
        }
    }
    void FixedUpdate()
    {
        Scene scene = SceneManager.GetActiveScene();
        string s = scene.name; // name of scene
        if (s != "MainScene")
        {
            float value = 0;
            if (!bang)
            {
                value = Input.GetAxis("Horizontal");
            }
            if (Mathf.Abs(value) > 0)
            {
                Vector2 vel = myBody.velocity;
                vel.x = value * speed;
                myBody.velocity = vel;
            }
            my_pos = this.transform.position;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (value < 0)
            {
                sr.flipX = true;
            }
            else if (value > 0)
            {
                sr.flipX = false;
            }
            Vector3 from = transform.position + Vector3.up * 0.3f;
            Vector3 to = transform.position + Vector3.down * 0.1f;
            int layer_id = 1 << LayerMask.NameToLayer("Ground");

            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
            if (hit)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (hit)
            {
                //Перевіряємо чи ми опинились на платформі
                if (hit.transform != null
                && hit.transform.GetComponent<MovingPlatform>() != null)
                {
                    //Приліпаємо до платформи
                    SetNewParent(this.transform, hit.transform);
                }
            }
            else
            {
                //Ми в повітрі відліпаємо під платформи
                SetNewParent(this.transform, this.heroParent);
            }
            Debug.DrawLine(from, to, Color.red);
            if (!bang)
            {
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    this.JumpActive = true;
                }

                if (this.JumpActive)
                {
                    //Якщо кнопку ще тримають
                    if (Input.GetButton("Jump"))
                    {
                        this.JumpTime += Time.deltaTime;
                        if (this.JumpTime < this.MaxJumpTime)
                        {
                            Vector2 vel = myBody.velocity;
                            vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                            myBody.velocity = vel;
                        }
                    }
                    else
                    {
                        this.JumpActive = false;
                        this.JumpTime = 0;
                    }
                }
            }
        }
    }
}
