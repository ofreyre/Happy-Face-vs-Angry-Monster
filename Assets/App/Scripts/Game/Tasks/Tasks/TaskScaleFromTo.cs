using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskScaleFromTo : TaskScaleTo, ITask
{
    public Vector3 m_valueStart;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskScaleFromTo");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        ((TaskScaleFromTo)task).m_valueStart = m_valueStart;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }
    }

    protected override void SetStartValues()
    {
        value0 = m_valueStart;
        value0K = m_valueStartK;
        value1 = m_valueEnd;
        value1K = m_valueEndK;
    }
}
