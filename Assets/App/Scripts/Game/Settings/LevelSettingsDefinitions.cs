using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct LevelSettingsScroll
{
    public float speedMin;
    public float speedMax;
    public float accelerationDuration;
    public float accelerationLapse;
}

[Serializable]
public struct LevelSettingsWall {
    public float force;
    public float bounceK;
    public float bounceMin;
}

[Serializable]
public struct LevelSettings
{
    public LevelSettingsScroll scroller;
    public LevelSettingsWall wallSides;
    public LevelSettingsWall wallBottom;
    public LevelSettingsWall wallTop;
}
