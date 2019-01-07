using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremor : MonoBehaviour {
    [SerializeField]
    float m_forceK;
    [SerializeField]
    float m_forceKmax = 0.5f;
    [SerializeField]
    float m_durationK;
    public static Tremor instance;
    float m_t;
    float m_force;
    Vector3 m_startPos;
    float m_direction = 1;
    Vector3 m_movement;
    bool m_shake = false;

    void Awake() {
        instance = this;
    }

    void Start() {
        m_startPos = transform.localPosition;
    }

    public void Shake(float force) {
        if (m_t < Time.time)
        {
            m_t = Time.time + m_durationK * force;
        }
        else {
            m_t += m_durationK * force;
        }
        m_force = Mathf.Min(m_force + force * m_forceK, m_forceKmax);
        m_movement = new Vector3(m_force, 0, 0);
        m_shake = true;
    }

    public void Update() {
        if (m_shake)
        {
            if (Time.time < m_t)
            {
                transform.localPosition = m_startPos + m_movement * m_direction;
                m_direction *= -1;
            }
            else {
                m_shake = false;
                transform.localPosition = m_startPos;
            }
        }
    }
}
