using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {
    
    public float m_speedMin;
    public float m_speedMax;
    public float m_accelerationDuration = 30;
    public float m_accelerationLapse = 0.1f;
    Vector3 m_velocity;
    float m_accelerationT0;
    float m_lapseT;
    bool m_accelerating = true;
    public float m_speed;
    public static Scroller instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        m_velocity = Vector3.up * m_speedMin;
        m_accelerationT0 = Time.time;
        m_lapseT = Time.time + m_accelerationLapse;
        EventManager.instance.Lose += OnLose;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_accelerating)
        {
            if (m_lapseT < Time.time)
            {
                m_speed = m_speedMin + (m_speedMax - m_speedMin) * (Time.time - m_accelerationT0) / m_accelerationDuration;
                if (m_speed >= m_speedMax) {
                    m_speed = m_speedMax;
                    m_accelerating = false;
                }
                m_velocity = Vector3.up * m_speed;
                m_lapseT = Time.time + m_accelerationLapse;
            }
        }
        transform.Translate(m_velocity * Time.deltaTime);
    }

    void OnLose() {
        enabled = false;
    }
}
