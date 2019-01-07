using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForceField : Destructible, IExploderContainer
{
    public Exploder m_exploder;
    public INVENTORYITEM_TYPE m_itemType;
    public GHOST_TYPE m_ghostType;
    public float m_staminaToBlink = 1;
    bool m_blinking;
    SpriteRenderer local_spr;
    Geometry local_geometry;
    Color m_originalColor;

    void Awake()
    {
        local_spr = GetComponent<SpriteRenderer>();
        m_originalColor = local_spr.color;
    }

    void Start()
    {
        m_stamina = 0;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        StopBlink();
    }

    void OnDisable()
    {
        GetComponent<TaskRun>().enabled = false;
    }

    public Exploder exploder
    {
        get { return m_exploder; }
        set { m_exploder = value; }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        local_geometry = col.gameObject.GetComponent<Geometry>();
        if (local_geometry != null && (local_geometry.m_ghostType == m_ghostType || m_ghostType == GHOST_TYPE.all))
        {
            m_exploder.Explode(col, m_damage, Vector3.positiveInfinity);
            if (Damage(col.gameObject.GetComponent<Destructible>().m_damage))
            {
                EventManager.instance.DispatchShieldExhausted();
            }
            else if(!m_blinking && m_stamina < m_staminaToBlink)
            {
                m_blinking = true;
                GetComponent<TaskRun>().Run(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        local_geometry = col.gameObject.GetComponent<Geometry>();
        if (local_geometry != null && (local_geometry.m_ghostType == m_ghostType || m_ghostType == GHOST_TYPE.all))
        {
            m_exploder.Explode(col.transform, m_damage, col.transform.position, col.transform.position - transform.position);
            if (Damage(col.gameObject.GetComponent<Destructible>().m_damage))
            {
                EventManager.instance.DispatchShieldExhausted();
            }
            else if (!m_blinking && m_stamina < m_staminaToBlink)
            {
                m_blinking = true;
                GetComponent<TaskRun>().Run(1);
            }
        }
    }

    public float stamina {
        get { return m_stamina; }
        set {
            m_stamina = value;
            if (m_blinking && m_stamina > m_staminaToBlink) {
                StopBlink();
            }
        }
    }

    void RestoreColor()
    {
        local_spr.color = m_originalColor;
    }

    void StopBlink()
    {
        GetComponent<TaskRun>().enabled = false;
        RestoreColor();
        m_blinking = false;
    }
}
