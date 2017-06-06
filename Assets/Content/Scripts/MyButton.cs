using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MyButton : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();
    bool clicked = false;
    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
    void Start()
    {
        MyButton playButton = GetComponent<MyButton>();
        playButton.signalOnClick.AddListener(this.onPlay);
        
    }
    private void Update()
    {
       
    }
    void onPlay()
    {
        SceneManager.LoadScene("ChooseLevel");
    }
}