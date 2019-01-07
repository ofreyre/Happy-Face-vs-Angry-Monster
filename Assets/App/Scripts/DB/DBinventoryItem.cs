using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum INVENTORYITEM_TYPE {
    berry,
    orange,
    apple,
    lemmon,
    pineapple,
    cherry,
    pill,
    rocket,
    pacman,
    misspacman,
    granade,
    granade_super,
    bomb,
    bomb_mega,
    forcefield_lightblue,
    forcefield_pink,
    forcefield_red,
    forcefield_yellow,
    forcefield_fushcia,
    forcefield_universal,
    cannon,
    cannon_double,
    cannon_triple,
    cannon_frontback,
    cannon_t,
    cannon_cross,
}

public enum INVENTORYITEM_CATEGORY{
    stamina,
    ammo,
    bomb,
    weapon,
    shield,
    character
 }

[Serializable]
public class DBinventoryItem: ScriptableObject {
    public INVENTORYITEM_TYPE type;
    public INVENTORYITEM_CATEGORY category;
    public float amount;
    new public string name;
    public string description;
    public Sprite m_inventoryIcon;
    public GameObject m_prefab;
    public int price;
    public int inventoryMax;
    public bool visibleInInventory = true;

    public static int CompareDBinventoryItem(DBinventoryItem a, DBinventoryItem b) {
        if (a.category < b.category) return -1;
        else if (a.category > b.category) return 1;
        else
        {
            if (a.price < b.price) return -1;
            else if (a.price > b.price) return 1;
            return 0;
        }
    }
}
