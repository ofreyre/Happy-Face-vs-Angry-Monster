using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDelegateImpulseToTarget : TaskDelegate
{
    public Transform m_target;
    public float m_impulse = 0.1f;
    public bool m_remove = true;

    public override void Delegate()
    {
        GetComponent<Rigidbody2D>().AddForce((m_target.position - transform.position).normalized * m_impulse, ForceMode2D.Impulse);
        if (m_remove)
        {
            Destroy(this);
        }
    }
}
