using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskParallel : Task {
    public Task[] m_tasks;
    bool[] m_end;
    int endCount;
    TaskParallel local_taskParallel;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskParallel");
        base.CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            m_end = new bool[m_tasks.Length];
            endCount = 0;
            return this;
        }
        else
        {
            //TaskParallel instance = (TaskParallel)Instantiate();
            local_taskParallel = (TaskParallel)Instantiate();
            int n = m_tasks.Length;
            local_taskParallel.m_tasks = new Task[n];
            {
                for (int i = 0; i < n; i++)
                {
                    local_taskParallel.m_tasks[i] = (Task)(m_tasks[i].Init(transform, force));
                }
            }
            return local_taskParallel.Init(transform, force, first);
        }
    }

    public override bool Run()
    {
        bool notEnd;
        for (int i = 0, n = m_tasks.Length; i < n; i++)
        {
            if (!m_end[i]) {
                notEnd = m_tasks[i].Run();
                if (!notEnd)
                {
                    m_end[i] = notEnd;
                    endCount++;
                    if (endCount == m_tasks.Length)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
