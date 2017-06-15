using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalResults : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject h;
        if (HeroController.redCryst == true)
        {
            h = GameObject.Find("CrystalEmptyM");
            Destroy(h.gameObject);
        }
        if (HeroController.blueCryst == true)
        {
            h = GameObject.Find("CrystalEmptyM2");
            Destroy(h.gameObject);

        }
        if (HeroController.greenCryst == true)
        {
            h = GameObject.Find("CrystalEmptyM3");
            Destroy(h.gameObject);

        }
        if (HeroController.redCryst2 == true)
        {
            h = GameObject.Find("CrystalEmptyM");
            Destroy(h.gameObject);
        }
        if (HeroController.blueCryst2 == true)
        {
            h = GameObject.Find("CrystalEmptyM2");
            Destroy(h.gameObject);

        }
        if (HeroController.greenCryst2 == true)
        {
            h = GameObject.Find("CrystalEmptyM3");
            Destroy(h.gameObject);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
