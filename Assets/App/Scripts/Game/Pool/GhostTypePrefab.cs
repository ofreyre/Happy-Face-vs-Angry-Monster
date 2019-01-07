using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GHOST_TYPE
{
    none,
    lightblue,
    pink,
    red,
    yellow,
    lightblue_super,
    pink_super,
    red_super,
    yellow_super,
    fushcia_super,
    all
}

[Serializable]
public struct GhostTypePrefab {
    public GHOST_TYPE type;
    public GameObject prefab;
}
