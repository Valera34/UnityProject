using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StopButton : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();
    public UnityEvent signalOnClick2 = new UnityEvent();
    public UnityEvent sound = new UnityEvent();
    public UnityEvent music = new UnityEvent();
    bool clicked = false;
    public GameObject settingsPrefab;
    public GameObject soundOff;
    public GameObject musicOff;
    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
    public void close()
    {
        this.signalOnClick2.Invoke();
    }
    public void _sound()
    {
        this.sound.Invoke();
    }
    public void _music()
    {
        this.music.Invoke();
    }
    void Start()
    {
        StopButton stopButton = GetComponent<StopButton>();
        stopButton.signalOnClick.AddListener(this.showSettings);
        stopButton.signalOnClick2.AddListener(this.destroySettings);
        stopButton.sound.AddListener(this.buttonSound);
        stopButton.music.AddListener(this.buttonMusic);
        soundOff = GameObject.Find("SoundOff");
        musicOff = GameObject.Find("MusicOff");
    }
    private void Update()
    {
        if (SoundManager.Instance.isSoundOn())
        {
            if (soundOff != null)
            {
                soundOff.gameObject.SetActive(false);
            }

        }
        else
        {
            if (soundOff != null)
            {
                soundOff.gameObject.SetActive(true);
            }
        }
        if (MusicManager.Instance2.isSoundOn())
        {
            if (musicOff != null)
            {
                musicOff.gameObject.SetActive(false);
            }

        }
        else
        {
            if (musicOff != null)
            {
                musicOff.gameObject.SetActive(true);
            }
        }
    }
    void showSettings()
    {
       
        if (GameObject.Find("SettingsPopUp") == null)
        {
            //Знайти батьківський елемент
            GameObject parent = UICamera.first.transform.parent.gameObject;
            //Створити Prefab
            GameObject obj = NGUITools.AddChild(parent, settingsPrefab);
            //Отримати доступ до компоненту (щоб передати параметри)
            //    SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();
            //...
        }
       

    }
    void buttonSound()
    {
        
            SoundManager.Instance.setSoundOn(!SoundManager.Instance.isSoundOn());
    }
    void buttonMusic()
    {
        MusicManager.Instance2.setSoundOn(!MusicManager.Instance2.isSoundOn());
    }
  
        void destroySettings()
    {
        GameObject h;
        h = GameObject.Find("SettingsPopUp");
       
        if (h != null)
        {
            Destroy(h.gameObject);
        }
    }
}