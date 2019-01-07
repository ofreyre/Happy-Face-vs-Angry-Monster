using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRotateFromToRnd : TaskRotateFromTo, ITask
{
    public Vector3 m_valueStartMax;
    public Vector3 m_valueEndMax;
    TaskRotateFromToRnd local_taskRotateFromToRnd;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskRotateFromToRnd");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_taskRotateFromToRnd = (TaskRotateFromToRnd)task;
        local_taskRotateFromToRnd.m_valueStartMax = m_valueStartMax;
        local_taskRotateFromToRnd.m_valueEndMax = m_valueEndMax;
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
            return _Instantiate("TaskRotateFromToRnd").Init(transform, force, first);
        }
    }

    protected override void SetStartValue()
    {
        value0 = RandomUtils.Range(m_valueStart, m_valueStartMax);
        value0K = m_valueStartK;
        value1 = RandomUtils.Range(m_valueEnd, m_valueEndMax);
        value1K = m_valueEndK;
    }
}
