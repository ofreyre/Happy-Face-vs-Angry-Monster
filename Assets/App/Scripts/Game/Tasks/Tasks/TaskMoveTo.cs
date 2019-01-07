using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMoveTo : TaskInterval
{
    public float m_valueStartK;
    public Vector3 m_valueEnd;
    public float m_valueEndK;

    protected Vector3 value0;
    protected float value0K;
    protected Vector3 value1;
    protected float value1K;
    Vector3 valueD;
    TaskMoveTo local_taskKoveTo;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskMoveTo");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_taskKoveTo = (TaskMoveTo)task;
        local_taskKoveTo.m_valueEnd = m_valueEnd;
        local_taskKoveTo.m_valueEndK = m_valueEndK;
        local_taskKoveTo.m_valueStartK = m_valueStartK;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            SetStartValues();
            valueD = (value1 * value1K - value0 * value0K) * (m_useForce?force:1);
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }
    }

    protected virtual void SetStartValues()
    {
        value0 = m_transform.localPosition;
        value0K = m_valueStartK;
        value1 = m_valueEnd;
        value1K = m_valueEndK;
    }

    public override bool Run()
    {
        bool notEnd = base.Run();
        if (notEnd)
        {
            m_transform.localPosition = value0 + valueD * k;
        }
        else
        {
            m_transform.localPosition = value1;
        }
        return notEnd;
    }
}
