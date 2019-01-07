using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DBlevels
{
    public bool[] unlocked;
    public int[] score;
    public int[] stars;

    public DBlevels(bool[] unlocked, int[] score, int[] stars)
    {
        this.unlocked = unlocked;
        this.score = score;
        this.stars = stars;
    }
}