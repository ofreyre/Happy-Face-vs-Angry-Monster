using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableShield : Collectable
{
    public INVENTORYITEM_TYPE m_itemType;
    public GHOST_TYPE m_ghostType;
    public float m_amount = 60;

    protected override void Collected()
    {
        EventManager.instance.DispatchShieldCollected(this);
        //Dont destroy to avoid GC
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    protected override void NotCollected()
    {
        //Dont destroy to avoid GC
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
