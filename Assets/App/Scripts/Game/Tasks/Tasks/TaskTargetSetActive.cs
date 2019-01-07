using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskTargetSetActive : Task, ITask {
    
    public bool m_activate;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskTargetSetActive");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        ((TaskTargetSetActive)task).m_activate = m_activate;
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
            return Instantiate().Init(transform, force, first);
        }
    }

    public override bool Run()
    {
        m_transform.gameObject.SetActive(m_activate);
        return false;
    }
}
