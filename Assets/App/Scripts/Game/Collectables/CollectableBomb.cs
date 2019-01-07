using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBomb : Collectable
{
    public float m_power;
    public float m_radius;
    public GameObject m_prefab;

    protected override void Collected()
    {
        EventManager.instance.DispatchBombCollected(this);

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
