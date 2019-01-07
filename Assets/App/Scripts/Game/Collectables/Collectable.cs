using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Destructible
{
    public Exploder m_exploder;
    public LayerMask m_collectors;
    Destructible local_destructible;

    protected bool IsCollector(int layer) {
        return (m_collectors.value & 1 << layer) == 1 << layer;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        local_destructible = col.gameObject.GetComponent<Destructible>();
        if (local_destructible != null)
        {
            m_exploder.Explode(col, local_destructible.m_damage, col.transform.position);
            if (Damage(col.gameObject.GetComponent<Destructible>().m_damage))
            {
                if (IsCollector(col.gameObject.layer))
                {
                    Collected();
                }
                else {
                    NotCollected();
                }
            }
        }
    }

    protected virtual void Collected()
    {
        Debug.Log("Collectable.OnCollisionEnter2D: Collected");
    }

    protected virtual void NotCollected()
    {
        Debug.Log("Collectable.OnCollisionEnter2D: NotCollected");
    }

}
