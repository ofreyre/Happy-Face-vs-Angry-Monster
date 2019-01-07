using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRelativeToTarget : UserVector
{
    public Transform m_target;
    Coroutine m_input;
    float m_radiansOffset;
    float m_t;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        m_radiansOffset = m_degreesOffset * Mathf.Deg2Rad;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (m_audioOn && Input.GetMouseButtonDown(0))
            {
                m_shootClipPlayer.Play();
            }
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = m_target.position.z;
            v -= m_target.position;
            float angle = Mathf.Atan2(v.y, v.x) * m_scale + m_radiansOffset;
            v.x = Mathf.Cos(angle);
            v.y = Mathf.Sin(angle);

            if (v != Vector3.zero)
            {
                if(NewVector != null) NewVector(v);
                if (Time.time > m_t)
                {
                    if (NewVectorLapse != null) NewVectorLapse(v);
                    m_t = Time.time + m_lapse;
                }
            }
        }
        else if (m_audioOn && Input.GetMouseButtonUp(0))
        {
            m_shootClipPlayer.Pause();
        }
    }
}
