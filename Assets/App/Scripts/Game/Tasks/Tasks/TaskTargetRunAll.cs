using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskTargetRunAll : Task, ITask
{
    public GameObject m_target;
    Task[] m_tasks;
    ITask taskItem;

    public override ITask Instantiate()
    {
        return _Instantiate("TaskTargetRunAll");
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
            return _Instantiate("TaskTargetRunAll").Init(transform, force, first);
        }
    }

    public override bool Run()
    {
        m_tasks = m_target.GetComponents<Task>();
        for (int i = 0, n = m_tasks.Length; i < n; i++) {
            taskItem = m_tasks[i];
            taskItem.Init(m_transform, m_force);
            taskItem.Run();
        }
        return false;
    }
}
