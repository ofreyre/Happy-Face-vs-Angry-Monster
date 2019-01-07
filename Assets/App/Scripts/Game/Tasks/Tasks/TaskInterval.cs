using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskInterval : Task
{
    public bool m_useForceForDuration = true;
    public float m_durationK;
    protected float duration_1;
    protected float t1;
    protected float k;
    TaskInterval local_taskInterval;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskInterval");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_taskInterval = (TaskInterval)task;
        local_taskInterval.m_useForceForDuration = m_useForceForDuration;
        local_taskInterval.m_durationK = m_durationK;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            float duration = GetDuration(m_useForceForDuration ? force : 1);
            duration_1 = 1 / duration;
            t1 = Time.time + duration;
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }

    }

    protected virtual float GetDuration(float force) {
        return force * m_durationK;
    } 

    public override bool Run()
    {
        float t = Time.time;
        k = Math.Min(1,1 - (t1 - t) * duration_1);
        if (k > 1) {
            Debug.Log("eeeeee " +t1+" "+ t+" "+(1/ duration_1));
            Debug.Log(k + " " + (t1 - t));
        }
        return t1 > t;
    }
}
