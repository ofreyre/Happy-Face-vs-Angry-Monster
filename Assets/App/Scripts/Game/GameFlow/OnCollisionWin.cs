using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class OnCollisionWin : OnCollisionGiveStar
{
    // Use this for initialization
    void Start () {

    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == m_colliderName)
        {
            base.OnTriggerEnter2D(col);
            EventManager.instance.DispatchWin();
        }
    }
}
