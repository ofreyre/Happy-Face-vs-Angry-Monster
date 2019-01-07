using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Swipe : UserVector
{   
    public float m_distance = 50;
    Vector3 m_startPoint;
    float m_distance2;

    // Use this for initialization
    protected override void Start () {
        m_distance2 = m_distance * m_distance;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            m_startPoint = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 p = Input.mousePosition - m_startPoint;
            //Jump
            if (p.sqrMagnitude > m_distance2)
            {
                NewVector(p);
            }
        }
    }
}
