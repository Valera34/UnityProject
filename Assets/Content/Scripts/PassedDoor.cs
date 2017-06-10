using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedDoor : MonoBehaviour {
    public GameObject level1;
    GameObject crystals;
    GameObject fruits;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start () {
        level1 = GameObject.Find("Level1Passed");
        crystals = GameObject.Find("EmptyCrystal");
        fruits = GameObject.Find("EmptyFruit");
        LevelStats stats = new LevelStats();
        string str = PlayerPrefs.GetString("stats");
        stats = JsonUtility.FromJson<LevelStats>(str);

        if (stats.levelPassed==true)
        {
            level1.gameObject.SetActive(true);
        }
        if (stats.levelPassed==false)
        {
            level1.gameObject.SetActive(false);
        }
        if (stats.hasCrystals == true)
        {
            crystals.gameObject.SetActive(false);
        }
        if (stats.hasCrystals == false)
        {
            crystals.gameObject.SetActive(true);
        }
        if (stats.hasAllFruits == true)
        {
            fruits.gameObject.SetActive(false);
        }
        if (stats.hasAllFruits == false)
        {
            fruits.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
