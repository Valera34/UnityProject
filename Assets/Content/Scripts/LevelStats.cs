using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStats
{
    public bool hasCrystals = false;
    public bool hasAllFruits = false;
    public bool levelPassed = false;
    public List<string> collectedFruits = new List<string>();
}

