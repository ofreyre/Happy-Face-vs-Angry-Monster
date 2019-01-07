using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskSpriteColorTo : TaskInterval, ITask
{
    public float m_valueStartK;
    public Color m_valueEnd;
    public float m_valueEndK;
    
    protected Color value0;
    protected float value0K;
    protected Color value1;
    protected float value1K;
    Color valueD;
    SpriteRenderer m_renderer;
    TaskSpriteColorTo local_spriteColorTo;

    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskSpriteColorTo");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }

    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        local_spriteColorTo = (TaskSpriteColorTo)task;
        local_spriteColorTo.m_valueStartK = m_valueStartK;
        local_spriteColorTo.m_valueEnd = m_valueEnd;
        local_spriteColorTo.m_valueEndK = m_valueEndK;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            m_renderer = transform.GetComponent<SpriteRenderer>();
            SetStartValues();
            valueD = (value1 * value1K - value0 * value0K) * (m_useForce ? force : 1);
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }
    }

    protected virtual void SetStartValues()
    {
        value0 = m_renderer.color;
        value0K = m_valueStartK;
        value1 = m_valueEnd;
        value1K = m_valueEndK;
    }

    public override bool Run()
    {
        bool notEnd = base.Run();
        if (notEnd)
        {
            m_renderer.color = value0 + valueD * k;
        }
        else
        {
            m_renderer.color = value1;
        }
        return notEnd;
    }
}
