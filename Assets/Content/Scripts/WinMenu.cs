using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinMenu : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();
    public UnityEvent signalOnClick2 = new UnityEvent();
    bool clicked = false;
    
    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
    public void close()
    {
        this.signalOnClick2.Invoke();
    }
    void Start()
    {
        WinMenu stopButton = GetComponent<WinMenu>();
        stopButton.signalOnClick.AddListener(this.retry);
        stopButton.signalOnClick2.AddListener(this.quit);

    }
    private void Update()
    {
       
    }
    void retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        string s = scene.name; // name of scene
        if (s == "Level1")
        {
            SceneManager.LoadScene("Level1");
        }
        if (s == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }
        allToNull();
    }
    void quit()
    {
        GameObject h;
        GameObject d;
        h = GameObject.Find("WinMenu");
        d = GameObject.Find("LoseMenu");
        if (h != null)
        {
            Destroy(h.gameObject);
            SceneManager.LoadScene("MainScene");
            allToNull();
        }
        if (d != null)
        {
            Destroy(d.gameObject);
            SceneManager.LoadScene("MainScene");
            allToNull();
        }
    }
    void allToNull()
    {
        FruitsMenu.fruit = 0;
        HeroController.redCryst = false;
        HeroController.blueCryst = false;
        HeroController.greenCryst = false;
        InterfaceCollect.coins_quantity = 0;
    }
}
