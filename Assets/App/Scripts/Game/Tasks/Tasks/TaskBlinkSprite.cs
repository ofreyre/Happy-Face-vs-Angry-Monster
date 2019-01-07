using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBlinkSprite : TaskInterval
{
    public float m_lapse = 1;
    float m_blinkT;
    SpriteRenderer m_renderer;
    bool m_visible;


    public override ITask Instantiate()
    {
        local_taskInstance = (Task)_Instantiate("TaskBlinkSprite");
        CopyParams(local_taskInstance);
        return local_taskInstance;
    }


    public override void CopyParams(Task task)
    {
        base.CopyParams(task);
        TaskBlinkSprite taskLocal = (TaskBlinkSprite)task;
        taskLocal.m_lapse = m_lapse;
    }

    public override ITask Init(Transform transform, float force, bool first = true)
    {
        if (m_instance)
        {
            base.Init(transform, force);
            m_blinkT = Time.time + m_lapse;
            if (m_renderer == null)
            {
                m_renderer = transform.GetComponent<SpriteRenderer>();
            }
            m_visible = true;
            return this;
        }
        else
        {
            return Instantiate().Init(transform, force, first);
        }
    }

    public override bool Run()
    {
        float t = Time.time;
        if (m_blinkT < t) {
            m_visible = !m_visible;
            Color color = m_renderer.color;
            color.a = m_visible ? 1 : 0;
            m_renderer.color = color;
            m_blinkT = t + m_lapse;
        }

        return base.Run();
    }
}
