using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAmmo : Collectable
{
    public int m_amount;
    public int m_shooterIndex;

    protected override void Collected()
    {
        EventManager.instance.DispatchAmmoCollected(this);

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
