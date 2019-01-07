using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromTo : MonoBehaviour {

    public Vector3 m_from;
    public Vector3 m_to;
    public float m_duration;
    float m_t;
    float m_d;

    // Use this for initialization
    void Start () {
        transform.position = m_from;
        m_t = Time.time + m_duration;
        m_d = 1 / m_duration;
    }
	
	// Update is called once per frame
	void Update () {
        float k = (m_t - Time.time) * m_d;
        transform.position = k * m_from + (1 - k) * m_to;
        if (Time.time > m_t)
        {
            m_t = Time.time + m_duration;
        }
    }
}
