using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceCollect : MonoBehaviour {
   // public UILabel label;
    // Use this for initialization
    void Start () {
        UILabel lbl = GetComponent<UILabel>();
        Scene scene = SceneManager.GetActiveScene();
        string s = scene.name; // name of scene
        if (s == "ChooseLevel")
        {
            lbl.text = PlayerPrefs.GetFloat("coins", 0).ToString();
        }
        else
        {
            lbl.text = "0000";
        }
    }
   public static bool accept = false;
    
     public static float coins_quantity = 0f;
    // Update is called once per frame
    void Update () {
          
    UILabel lbl = GetComponent<UILabel>();
        if (accept)
        {
            
            coins_quantity = coins_quantity + 1;
            if (coins_quantity >=0)
            {
                lbl.text = "000" + coins_quantity.ToString();
            }
            if (coins_quantity >= 10)
            {
                lbl.text = "00" + coins_quantity.ToString();
            }
            if (coins_quantity >= 100)
            {
                lbl.text = "0" + coins_quantity.ToString();
            }
            if (coins_quantity >= 1000)
            {
                lbl.text =coins_quantity.ToString();
            }
            accept = false;
        }
        
    }

}
