using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAccumulativeRelativeToTarget : UserVector
{
    public Transform m_target;
    float m_startAngle;
    float m_currentAngle;
    Vector3 m_startDirection;
    bool m_mouseDown;
    float m_t;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        m_startAngle = m_degreesOffset;
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_mouseDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_audioOn)
                {
                    m_shootClipPlayer.Play();
                }
                m_startDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_startDirection.z = m_target.position.z;
                m_startDirection -= m_target.position;
                m_mouseDown = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (m_audioOn)
                {
                    m_shootClipPlayer.Pause();
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                v.z = m_target.position.z;
                v -= m_target.position;
                float angle = Vector3.Angle(m_startDirection, v);
                if (Vector3.Cross(m_startDirection, v).z < 0) {
                    angle = 360-angle;
                }
                angle *= m_scale;
                m_currentAngle = m_startAngle + angle * Mathf.Deg2Rad;

                v.x = Mathf.Cos(m_currentAngle);
                v.y = Mathf.Sin(m_currentAngle);

                if (v != Vector3.zero)
                {
                    NewVector(v);
                    if (Time.time > m_t) {
                        NewVectorLapse(v);
                        m_t = Time.time + m_lapse;
                    }
                }
            }else
            {
                if (m_audioOn)
                {
                    m_shootClipPlayer.Pause();
                }
                m_startAngle = m_currentAngle;
                m_mouseDown = false;
            }
        }
    }
}
