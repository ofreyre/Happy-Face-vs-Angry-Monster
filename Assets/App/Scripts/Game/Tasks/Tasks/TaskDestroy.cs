using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDestroy : Task, ITask
{

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskDestroy");
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
        Destroy(m_transform.gameObject);
        return false;
    }
}
