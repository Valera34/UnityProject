using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager
{
    bool is_sound_on = true;
    public bool isSoundOn()
    {
        return this.is_sound_on;
    }
    public void setSoundOn(bool val)
    {
        this.is_sound_on = val;
        PlayerPrefs.SetInt("music", this.is_sound_on ? 1 : 0);
        PlayerPrefs.Save();
    }
    public MusicManager()
    {
        is_sound_on = PlayerPrefs.GetInt("music", 1) == 1;
    }
    public static MusicManager Instance2 = new MusicManager();
}
