using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRays : MonoBehaviour {

    public SpriteRenderer m_rayTop;
    public SpriteRenderer m_rayBottom;
    public Color m_colorMax;
    public Color m_colorMin;
    Color m_colorD;
    float heightInv;

    float m_top, m_bottom;

    // Use this for initialization
    void Start ()
    {
        m_top = Camera.main.orthographicSize;
        m_bottom = -m_top;
        heightInv = 1 / m_top * 0.5f;
        m_colorD = m_colorMax - m_colorMin;
    }
	
	// Update is called once per frame
	void Update () {
        m_rayTop.color = m_colorMin + m_colorD * (transform.localPosition.y - m_bottom) * heightInv;
        m_rayBottom.color = m_colorMin + m_colorD * (m_top - transform.localPosition.y) * heightInv;
    }
}
