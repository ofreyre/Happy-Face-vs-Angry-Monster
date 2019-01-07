using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shield : Destructible, IExploderContainer
{
    public Exploder m_exploder;
    float m_shield0;
    float m_stamina0;
    float m_damage0;
    bool m_started;
    Geometry local_geometry;

    public Exploder exploder
    {
        get { return m_exploder; }
        set { m_exploder = value; }
    }

    private void Start()
    {
        m_shield0 = m_shield;
        m_stamina0 = m_stamina;
        m_damage0 = m_damage;
        m_started = true;
    }

    private void OnEnable()
    {
        if (m_started)
        {
            m_shield = m_shield0;
            m_stamina = m_stamina0;
            m_damage = m_damage0;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        local_geometry = col.gameObject.GetComponent<Geometry>();
        if (local_geometry != null)
        {
            m_exploder.Explode(col, m_damage, Vector3.positiveInfinity);
            if (Damage(col.gameObject.GetComponent<Destructible>().m_damage)) {
                gameObject.SetActive(false);
            }
        }
    }
}
