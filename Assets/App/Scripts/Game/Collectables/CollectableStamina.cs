using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStamina : Collectable
{
    public float m_amount;

    protected override void Collected()
    {
        EventManager.instance.DispatchStaminaCollected(this);
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
