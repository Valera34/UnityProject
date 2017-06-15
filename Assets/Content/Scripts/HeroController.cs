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
    public AudioClip music = null;
    AudioSource musicSource = null;
    public AudioClip music2 = null;
    AudioSource musicSource2 = null;
    public  AudioClip music3 = null;
    AudioSource musicSource3 = null;
    public static float count=0;
    void Awake()
    {
        l = GameObject.FindGameObjectsWithTag("Fruit").Length;
        lastRabit = this;
        loseLevel = false;
        
    }
    public static float l;
   static GameObject  heart3;
      static  GameObject heart2;
    static GameObject heart;
    void Start()
    {
        life = 3;
         heart3 = GameObject.Find("Heart3");
         heart2 = GameObject.Find("Heart2");
         heart = GameObject.Find("Heart");
        SoundManager.Instance.setSoundOn(SoundManager.Instance.isSoundOn());
        MusicManager.Instance2.setSoundOn(MusicManager.Instance2.isSoundOn());
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource2 = gameObject.AddComponent<AudioSource>();
        musicSource2.clip = music2;
        musicSource3 = gameObject.AddComponent<AudioSource>();
        musicSource3.clip = music3;
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
    bool recover = false;
    public void Recover()
    {
        recover = true;
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
    public static bool redCryst=false;
    public static bool blueCryst = false;
    public static bool greenCryst = false;
    public static bool redCryst2 = false;
    public static bool blueCryst2 = false;
    public static bool greenCryst2 = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 my_pos2 = this.transform.position;
        if (collider.gameObject.name == "Room")
        {
            SceneManager.LoadScene("Level1");

        }
        if (collider.gameObject.name == "Room (1)")
        {
            LevelStats st = new LevelStats();
            string a = PlayerPrefs.GetString("stats");
            st = JsonUtility.FromJson<LevelStats>(a);
            if (st.levelPassed == true)
            {
                SceneManager.LoadScene("Level2");
            }

        }
        if (collider.gameObject.name == "Exit1")
        {
           
           
        }
       
       /* for (int i = 0; i < l; i++)
         {
        string s = "Fruit1";
            LevelStats stats = new LevelStats();
            string str = PlayerPrefs.GetString("stats");
            stats = JsonUtility.FromJson<LevelStats>(str);
            if (stats == null)
            {
                stats = new LevelStats();
            }
            stats.hasAllFruits = true;
            string v = JsonUtility.ToJson(stats);
            PlayerPrefs.SetString("stats", v);
            PlayerPrefs.Save();
            if (collider.gameObject.name == "Fruit1")
            {
           
                if (!stats.collectedFruits.Contains(s))
                {
                    stats.collectedFruits.Add(s);
                   
                }
            }

            }*/
        GameObject h;
        Scene scene = SceneManager.GetActiveScene();
        string s = scene.name;
        if (collider.gameObject.name == "RedCrystal")
        {
            h = GameObject.Find("CrystalEmpty");
            if (s == "Level1")
            {
                redCryst = true;
            }
            if (s == "Level2")
            {
                redCryst2 = true;
            }
            Destroy(h.gameObject);
        }
        if (collider.gameObject.name == "BlueCrystal")
        {
            h = GameObject.Find("CrystalEmpty2");
            if (s == "Level1")
            {
                blueCryst = true;
            }
            if (s == "Level2")
            {
                blueCryst2 = true;
            }
            Destroy(h.gameObject);

        }
        if (collider.gameObject.name == "GreenCrystal")
        {
            h = GameObject.Find("CrystalEmpty3");
            if (s == "Level1")
            {
                greenCryst = true;
            }
            if (s == "Level2")
            {
                greenCryst2 = true;
            }
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
                if (!musicSource3.isPlaying&&SoundManager.Instance.isSoundOn())
                    
                {
                    musicSource3.Play();
                }
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
                    j = false;
                }
                else
                {
                    animator.SetBool("jump", true);
                    j = true;
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
                if (!musicSource3.isPlaying && SoundManager.Instance.isSoundOn())
                {
                    musicSource3.Play();
                }
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
            if (DeathHere.fall == true)
            {
                if (!musicSource3.isPlaying && SoundManager.Instance.isSoundOn())
                {
                    musicSource3.Play();
                }
                DeathHere.fall = false;
            }

        }
    }
    public static bool loseLevel = false;
    public static Vector3 my_pos;
    public static void Health()
    {
        if (life == 2)
        {
            heart3.gameObject.SetActive(false);
        }
        if (life == 1)
        {
            heart2.gameObject.SetActive(false);

        }
        if (life == 0)
        {
            heart.gameObject.SetActive(false);
            loseLevel = true;
            life = 3;
        }
    }
    public static void Life()
    {
        GameObject h;
        if (life == 2)
        {
            heart3.gameObject.SetActive(true);
            life++;
        }
        if (life == 1)
        {

            heart2.gameObject.SetActive(true);
            life++;
        }
    }
    bool j = false;
    void FixedUpdate()
    {
        if (recover)
        {
            Life();
            recover = false;
        }
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
                if (!musicSource.isPlaying && SoundManager.Instance.isSoundOn())
                {
                    musicSource.Play();
                }
            }
            if(Mathf.Abs(value) ==0 || this.JumpActive || j)
            {
                if (musicSource.isPlaying)
                {
                    musicSource.Stop();
                }
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
                if (SoundManager.Instance.isSoundOn())
                {
                    musicSource2.Play();
                }
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
