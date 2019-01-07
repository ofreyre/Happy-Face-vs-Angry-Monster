using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRelativeToPoint : UserVector
{
    public Transform m_cursor;
    bool m_down;
    float m_t;
    Vector3 center = Vector3.zero;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        m_cursor.gameObject.SetActive(false);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_audioOn)
            {
                m_shootClipPlayer.Play();
            }
            center = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            center.z = 0;
            m_cursor.localPosition = center;
            m_cursor.gameObject.SetActive(true);

        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 v = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            v.z = center.z;
            v -= center;
            if (v != Vector3.zero)
            {
                NewVector(v);
                if (Time.time > m_t)
                {
                    NewVectorLapse(v);
                    m_t = Time.time + m_lapse;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (m_audioOn)
            {
                m_shootClipPlayer.Pause();
            }
            m_cursor.gameObject.SetActive(false);
        }
    }
}
