using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRotateFromTo : TaskRotateTo, ITask
{
    public Vector3 m_valueStart;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskRotateFromTo");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        //TaskRotateFromTo taskLocal = (TaskRotateFromTo)task;
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
            return _Instantiate("TaskRotateFromTo").Init(transform, force, first);
        }
    }

    protected override void SetStartValue()
    {
        value0 = m_valueStart;
        value0K = m_valueStartK;
        value1 = m_valueEnd;
        value1K = m_valueEndK;
    }
}
