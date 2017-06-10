using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsMenu : MonoBehaviour {

    void Start()
    {
        UILabel lbl = GetComponent<UILabel>();
        lbl.text ="0/" + HeroController.l.ToString();
    }
    public static bool accept2 = false;

    public static float fruit = 0f;
    // Update is called once per frame
    void Update()
    {

        UILabel lbl = GetComponent<UILabel>();
        if (accept2)
        {

            fruit = fruit + 1;
            lbl.text = fruit+ "/" + HeroController.l.ToString();
            accept2 = false;
        }

    }
}
