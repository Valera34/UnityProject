using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsResult : MonoBehaviour {

    void Start()
    {
        UILabel lbl = GetComponent<UILabel>();
        lbl.text = "+" +InterfaceCollect.coins_quantity.ToString();
        PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins", 0)+InterfaceCollect.coins_quantity);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
