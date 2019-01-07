using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskImpulse2D : Task, ITask
{
    public Vector3 m_value;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskImpulse2D");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        ((TaskImpulse2D)task).m_value = m_value;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            transform.GetComponent<Rigidbody2D>().AddForce(m_value * (m_useForce ? force : 1), ForceMode2D.Impulse);
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }
    }
}
