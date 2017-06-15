using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour {
    public GameObject win;
    public GameObject loseMenu;
    // Use this for initialization
    bool block = false;
    // Update is called once per frame
    public AudioClip music = null;
    AudioSource musicSource = null;
    public AudioClip music2 = null;
    AudioSource musicSource2 = null;
    public AudioClip music3 = null;
    AudioSource musicSource3 = null;
         LevelStats stats;
    LevelStats stats2;
    void Start()
    {
        musicSource3 = gameObject.AddComponent<AudioSource>();
        musicSource3.clip = music3;
        musicSource3.loop = true;
        string str = PlayerPrefs.GetString("stats", null);
        stats = JsonUtility.FromJson<LevelStats>(str);
        if (stats==null)
        {
            stats = new LevelStats();
        }
        string str2 = PlayerPrefs.GetString("stats2", null);
        stats2 = JsonUtility.FromJson<LevelStats>(str);
        if (stats2 == null)
        {
            stats2 = new LevelStats();
        }
       
    }
    bool exit = false;
    void Update () {
        if (MusicManager.Instance2.isSoundOn())
        {
            if (!musicSource3.isPlaying)
            {
                musicSource3.Play();
            }
        }
        if (!MusicManager.Instance2.isSoundOn())
        {
            musicSource3.Stop();
        }
            if (HeroController.loseLevel == true)
        {
            if (block == false&&GameObject.Find("LoseMenu") == null&& GameObject.Find("WinMenu") == null)
            {
                //Знайти батьківський елемент
                GameObject parent = UICamera.first.transform.parent.gameObject;
                //Створити Prefab
                GameObject obj = NGUITools.AddChild(parent, loseMenu);
                //Отримати доступ до компоненту (щоб передати параметри)
                //    SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();
                //...
                musicSource3.Stop();
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.clip = music;
                // musicSource.loop = true;
                if (MusicManager.Instance2.isSoundOn())
                {
                    musicSource.Play();
                }
                HeroController.loseLevel = false;
                block = true;
            }
        }
            if (exit)
         {
            Scene scene = SceneManager.GetActiveScene();
            string s = scene.name;
            if (s == "Level1")
            {
                if (HeroController.redCryst == true && HeroController.blueCryst == true && HeroController.greenCryst == true)
                {
                    stats.hasCrystals = true;
                }

                if (HeroController.count == HeroController.l)
                {
                    stats.hasAllFruits = true;
                }
                stats.levelPassed = true;
                string str = JsonUtility.ToJson(stats);
                PlayerPrefs.SetString("stats", str);
                PlayerPrefs.Save();
            }
            if (s == "Level2")
            {
                if (HeroController.redCryst2 == true && HeroController.blueCryst2 == true && HeroController.greenCryst2 == true)
                {
                    stats2.hasCrystals = true;
                }

                if (HeroController.count == HeroController.l)
                {
                    stats2.hasAllFruits = true;
                }
                stats2.levelPassed = true;
                string str2 = JsonUtility.ToJson(stats2);
                PlayerPrefs.SetString("stats2", str2);
                PlayerPrefs.Save();
            }
            }
    }
    void OnTriggerEnter2D(Collider2D collider)

    {
        if (collider.gameObject.name == "Rabit")
        {
           
            if (GameObject.Find("WinMenu") == null)
            {
                //Знайти батьківський елемент
                GameObject parent = UICamera.first.transform.parent.gameObject;
                //Створити Prefab
                GameObject obj = NGUITools.AddChild(parent, win);
                //Отримати доступ до компоненту (щоб передати параметри)
                //    SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();
                //...
                musicSource3.Stop();
                musicSource2 = gameObject.AddComponent<AudioSource>();
                musicSource2.clip = music2;
                
                // musicSource.loop = true;
                if (MusicManager.Instance2.isSoundOn())
                {
                    musicSource2.Play();
                }
                block = true;
                exit = true;
            }
        }
    }
    }
