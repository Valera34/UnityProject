using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsResult : MonoBehaviour
{

    void Start()
    {
        UILabel lbl = GetComponent<UILabel>();
        lbl.text = FruitsMenu.fruit+"/" + HeroController.l.ToString();
    }

    
    // Update is called once per frame
    void Update()
    {

     

    }
}
