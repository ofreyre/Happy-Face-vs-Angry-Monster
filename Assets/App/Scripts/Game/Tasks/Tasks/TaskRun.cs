using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRun : MonoBehaviour {

    public Task m_task;

    ITask task;
    float m_force;

    /*public virtual void Run(float force)
    {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        StartCoroutine(_Run(force));
    }

    IEnumerator _Run(float force) {
        ITask task = m_task.Init(transform, force, true);
        task.Init(transform, force, true);
        while (task != null) {
            if (!task.Run()) {
                task = task.GetNext();
                if (task != null)
                {
                    task.Init(transform, force, false);
                }
            }
            yield return null;
        }
    }*/
    
    public virtual void Run(float force)
    {
        m_force = force;
        task = m_task.Init(transform, force, true);
        task.Init(transform, force, true);
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        if (!enabled) {
            enabled = true;
        }
    }

    private void Update()
    {
        if (task != null && !task.Run())
        {
            task = task.GetNext();
            if (task != null)
            {
                task.Init(transform, m_force, false);
            }
        }
    }

    public ITask GetNext() {
        return null;
    }
}
