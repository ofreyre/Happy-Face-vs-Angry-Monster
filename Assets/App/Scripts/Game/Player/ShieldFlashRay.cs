using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFlashRay : MonoBehaviour, IEffect
{
    [SerializeField]
    Color m_ColorStart;
    [SerializeField]
    Color m_ColorEnd;
    [SerializeField]
    float m_DurationK = 0.1f;
    [SerializeField]
    float m_widthK = 0.1f;
    [SerializeField]
    float m_outDurationK = 0.05f;
    SpriteRenderer m_renderer;
    [SerializeField]
    ShieldFlash m_shieldFlash;
    float m_width;
    Vector3 m_startSize;

    public void Init() {
        m_renderer = GetComponent<SpriteRenderer>();
        m_startSize = m_renderer.size;
        m_startSize.x = 0;
        m_width = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }

    public void Run(float force)
    {
        gameObject.SetActive(true);
        StartCoroutine(_Run(force));
    }

    IEnumerator _Run(float force) {
        gameObject.SetActive(true);
        float width = m_widthK * m_width * force;
        float duration = m_DurationK * force;
        float t0 = Time.time;
        float t1 = t0 + duration;
        float k;
        Vector2 size = m_startSize;
        Color colorD = m_ColorEnd - m_ColorStart;
        while (t1 > Time.time) {
            k = (Time.time - t0) / duration;
            m_renderer.color = m_ColorStart + colorD * k;
            size.x = width * k;
            m_renderer.size = size;
            yield return null;
        }
        duration = m_outDurationK * force;
        t0 = Time.time;
        t1 = t0 + duration;
        colorD = m_ColorEnd;
        float a = m_ColorEnd.a;
        while (t1 > Time.time)
        {
            colorD.a = a * 1 - (Time.time - t0) / duration; 
            m_renderer.color = colorD;
            yield return null;
        }
        gameObject.SetActive(false);
        m_shieldFlash.RayFinished();
    }
}
