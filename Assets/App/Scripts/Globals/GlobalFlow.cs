using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;

public class GlobalFlow : ScriptableObject
{
    public Globals globals;
    public int stage;
    public int level;
    public string firstScene;
    public AdManager adManager;
    public string interstitial;
    string prevScene = null;
    string currentScene = null;

    public void Init()
    {
        prevScene = null;
        currentScene = null;
    }

    public int AbsoluteLevel {
        set {
            stage = value / globals.levelsPerStage;
            level = value % globals.levelsPerStage;
        }
        get {
            return stage * globals.levelsPerStage + level;
        }
    }

    public void ToScene(string name)
    {
        prevScene = currentScene;
        currentScene = name;
        if (name.IndexOf("level") == 0)
        {
            adManager.ShowInterstitial(interstitial);
        }
        System.GC.Collect();
        Application.LoadLevel(name);
    }

    public string PrevScene {
        get
        {
            if (prevScene == null) {
                prevScene = firstScene;
            }
            return prevScene;
        }
    }
}
