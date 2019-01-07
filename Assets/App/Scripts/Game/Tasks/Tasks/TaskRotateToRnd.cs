using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRotateToRnd : TaskRotateTo, ITask
{
    public Vector3 m_valueEndMax;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskRotateToRnd");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        ((TaskRotateToRnd)task).m_valueEndMax = m_valueEndMax;
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
            return _Instantiate("TaskRotateToRnd").Init(transform, force, first);
        }
    }

    protected override void SetStartValue()
    {
        value0 = m_transform.eulerAngles;
        value0K = m_valueStartK;
        value1 = RandomUtils.Range(m_valueEnd, m_valueEndMax);
        value1K = m_valueEndK;
    }
}
