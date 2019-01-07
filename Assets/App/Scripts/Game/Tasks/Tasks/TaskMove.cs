using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMove : TaskInterval
{
    public Vector3 m_velocity;
    Vector3 m_valueD;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskMove");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        ((TaskMove)task).m_velocity = m_velocity;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            m_valueD = m_velocity * (m_useForce ? force : 1);
            return this;
        }
        else
        {
            return _Instantiate("TaskMove").Init(transform, force, first);
        }
    }

    public override bool Run()
    {
        m_transform.position += m_valueD * Time.deltaTime;
        return base.Run();
    }
}
