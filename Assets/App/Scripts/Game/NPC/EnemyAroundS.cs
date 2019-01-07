using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundS : Geometry
{
    public Transform m_target;
    public float m_targetTolerance;
    public Vector2 m_radius;
    public float m_speedPath;
    public float m_curves;
    public bool m_lookAtTarget;
    float m_radians = Mathf.PI * 0.5f;
    Vector3 m_center;
    Vector3 m_centerVelocity;
    bool m_activated;
    float m_radiandOffsetX;
    Vector3 m_prevPosition;
    Vector3 m_prevPositionPrev;
    public LayerMask m_activeLayer;


    void Start() {
    }
    
    public override void Activate()
    {
        if (!m_activated)
        {
            gameObject.layer = Mathf.RoundToInt(Mathf.Log(m_activeLayer.value, 2));
            m_activated = true;
            m_center = transform.position + new Vector3(transform.position.x, transform.position.y - m_radius.y, 0);
            m_radiandOffsetX = -Mathf.PI * 0.5f;
            m_prevPosition = transform.position;
            base.Activate();
        }
    }

    // Update is called once per frame
    void Update () {
        m_prevPositionPrev = m_prevPosition;
        m_prevPosition = transform.position;
        m_center = m_target.position;
        m_radians += m_speedPath * Time.deltaTime;
        transform.position = m_center + new Vector3(Mathf.Sin((m_radians + m_radiandOffsetX) * m_curves) * m_radius.x, Mathf.Sin(m_radians) * m_radius.y, 0);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //int comparison is faster then string. 11 = layer activator.
        //if (col.name != "activator")
        if (col.gameObject.layer != 11)
        {
            Vector3 v = transform.position - m_prevPosition;
            if (v == Vector3.zero) {
                v = transform.position - m_prevPositionPrev;
            }
            col.GetComponent<IExploderContainer>().exploder.Explode(transform, m_damage, transform.position, v);
        }
        else
        {
            Activate();
        }
    }
}
