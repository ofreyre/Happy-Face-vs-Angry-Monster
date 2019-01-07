using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Task: ScriptableObject,  ITask {
    public bool m_useForce = true;
    public int m_loops = 0;
    [HideInInspector]
    public bool m_instance;
    protected int m_loopI;
    protected float m_force = 1;
    protected Transform m_transform;
    protected Task local_taskInstance;


    public virtual ITask Instantiate()
    {   
        return null;
    }

    public virtual void CopyParams(Task task) {
        task.m_useForce = m_useForce;
        task.m_loops = m_loops;
    }

    protected ITask _Instantiate(string className)
    {
        local_taskInstance = (Task)CreateInstance(className);
        local_taskInstance.m_instance = true;
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public virtual ITask Init(Transform transform, float force, bool first = true)
    {
        m_transform = transform;
        m_force = m_useForce ? force: 1;
        return this;
    }

    public virtual bool Run() {
        return false;
    }

    public virtual ITask GetNext() {
        if (m_loopI < m_loops) {
            m_loopI++;
            return this;
        }
        return null;
    }
}
