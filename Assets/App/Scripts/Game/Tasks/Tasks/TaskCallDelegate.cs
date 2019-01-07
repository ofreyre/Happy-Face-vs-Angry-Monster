using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCallDelegate : Task
{
    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskCallDelegate");
        return local_taskInstance;
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
        m_transform.GetComponent<TaskDelegate>().Delegate();
        return false;
    }
}
