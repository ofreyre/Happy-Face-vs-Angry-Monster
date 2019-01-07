using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskSequence : Task, ITask
{
    public Task[] m_tasks;
    int m_taskI;
    ITask m_currentTask;
    TaskSequence local_instance;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskSequence");
        base.CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            if (first)
            {
                base.Init(transform, force);
                m_taskI = 0;
                m_loopI = 0;
            }
            m_currentTask = m_tasks[m_taskI];
            m_currentTask.Init(transform, force, first);
            return this;
        }
        else
        {
            local_instance = (TaskSequence)Instantiate();
            int n = m_tasks.Length;
            local_instance.m_tasks = new Task[n];
            {
                for (int i = 0; i < n; i++) {
                    local_instance.m_tasks[i] = (Task)(m_tasks[i].Init(transform, force, first));
                }
            }
            return local_instance.Init(transform, force, first);
        }
    }

    public override bool Run() {
        return m_currentTask.Run();
    }

    public override ITask GetNext() {
        if (m_taskI < m_tasks.Length - 1)
        {
            m_taskI++;
            return this;
        }
        else if (m_loopI < m_loops)
        {
            m_loopI++;
            m_taskI = 0;
            return this;
        }
        m_taskI = 0;
        m_loopI = 0;
        m_transform = null;
        return null;
    }
}
