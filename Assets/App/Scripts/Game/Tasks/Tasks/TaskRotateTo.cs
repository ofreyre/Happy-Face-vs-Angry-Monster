using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRotateTo : TaskInterval, ITask
{
    public float m_valueStartK;
    public Vector3 m_valueEnd;
    public float m_valueEndK;

    protected Vector3 value0;
    protected float value0K;
    protected Vector3 value1;
    protected float value1K;
    Vector3 valueD;
    TaskRotateTo local_rotateTo;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskRotateTo");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_rotateTo = (TaskRotateTo)task;
        local_rotateTo.m_valueStartK = m_valueStartK;
        local_rotateTo.m_valueEnd = m_valueEnd;
        local_rotateTo.m_valueEndK = m_valueEndK;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            SetStartValue();
            valueD = (value1 * value1K - value0 * value0K) * force;
            return this;
        }
        else
        {
            return _Instantiate("TaskRotateTo").Init(transform, force, first);
        }
    }

    protected virtual void SetStartValue()
    {
        value0 = m_transform.eulerAngles;
        value0K = m_valueStartK;
        value1 = m_valueEnd;
        value1K = m_valueEndK;
    }

    public override bool Run()
    {
        bool notEnd = base.Run();
        if (notEnd)
        {
            m_transform.eulerAngles = value0 + valueD * k;
        }
        else
        {
            m_transform.eulerAngles = value1;
        }
        return notEnd;
    }
}
