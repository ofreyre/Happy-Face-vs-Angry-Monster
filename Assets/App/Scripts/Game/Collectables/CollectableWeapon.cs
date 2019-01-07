using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWeapon : Collectable {

    public int m_amount;
    public INVENTORYITEM_TYPE m_type;

    protected override void Collected()
    {
        EventManager.instance.DispatchWeaponCollected(this);
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
