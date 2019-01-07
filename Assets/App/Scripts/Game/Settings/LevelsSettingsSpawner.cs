using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ArrayWraperInt
{
    public int[] array;
}

public class LevelsSettingsSpawner : ScriptableObject
{

    public ArrayWraperInt[] spawners;
}
