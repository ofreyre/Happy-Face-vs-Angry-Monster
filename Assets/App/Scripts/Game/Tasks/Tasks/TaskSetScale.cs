using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSetScale : Task, ITask {
    public Vector3 m_value;
    public float m_valueK;
    TaskSetScale local_taskLocal;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskSetScale");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_taskLocal = (TaskSetScale)task;
        local_taskLocal.m_value = m_value;
        local_taskLocal.m_valueK = m_valueK;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            transform.localScale = m_value * force * m_valueK;
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }
    }
}
