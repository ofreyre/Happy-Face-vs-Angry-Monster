using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskDelay : TaskInterval
{
    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskDelay");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            return this;
        }
        else {
            return Instantiate().Init(transform, force, first);
        }
    }
}
