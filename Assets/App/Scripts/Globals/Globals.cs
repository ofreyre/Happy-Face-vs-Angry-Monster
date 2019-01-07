using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : ScriptableObject
{
    public int stages = 6;
    public int levelsPerStage = 20;
    public int coinsPerVideo = 50;

    public int LevelsCount {
        get { return stages * levelsPerStage; }
    }
}
