using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSetRotationRnd : Task, ITask
{
    public Vector3 m_valueMin;
    public Vector3 m_valueMax;
    public float m_valueK;
    TaskSetRotationRnd local_taskLocal;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskSetRotationRnd");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_taskLocal = (TaskSetRotationRnd)task;
        local_taskLocal.m_valueMin = m_valueMin;
        local_taskLocal.m_valueMax = m_valueMax;
        local_taskLocal.m_valueK = m_valueK;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            transform.eulerAngles = RandomUtils.Range(m_valueMin, m_valueMax) * m_valueK;
            return this;
        }
        else
        {
            return _Instantiate("TaskSetRotationRnd").Init(transform, force, first);
        }
    }
}
