using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedDoor : MonoBehaviour {
    public GameObject level1;
    GameObject crystals;
    GameObject fruits;
     GameObject level2;
    GameObject crystals2;
    GameObject fruits2;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start () {
        level1 = GameObject.Find("Level1Passed");
        crystals = GameObject.Find("EmptyCrystal");
        fruits = GameObject.Find("EmptyFruit");

        level2 = GameObject.Find("Level2Passed");
        crystals2 = GameObject.Find("EmptyCrystal2");
        fruits2 = GameObject.Find("EmptyFruit2");

        LevelStats stats = new LevelStats();
        string str = PlayerPrefs.GetString("stats");
        stats = JsonUtility.FromJson<LevelStats>(str);

        LevelStats stats2 = new LevelStats();
        string str2 = PlayerPrefs.GetString("stats2");
        stats2 = JsonUtility.FromJson<LevelStats>(str2);
    /*   stats = new LevelStats();
     stats2 = new LevelStats();
        string str3 = JsonUtility.ToJson(stats2);
        PlayerPrefs.SetString("stats2", str3);
        PlayerPrefs.Save();
        string str4 = JsonUtility.ToJson(stats);
        PlayerPrefs.SetString("stats", str4);
        PlayerPrefs.Save();*/
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

        if (stats2.levelPassed == true)
        {
            level2.gameObject.SetActive(true);
        }
        if (stats2.levelPassed == false)
        {
            level2.gameObject.SetActive(false);
        }
        if (stats2.hasCrystals == true)
        {
            crystals2.gameObject.SetActive(false);
        }
        if (stats2.hasCrystals == false)
        {
            crystals2.gameObject.SetActive(true);
        }
        if (stats2.hasAllFruits == true)
        {
            fruits2.gameObject.SetActive(false);
        }
        if (stats2.hasAllFruits == false)
        {
            fruits2.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
