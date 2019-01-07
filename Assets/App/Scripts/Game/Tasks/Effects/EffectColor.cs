using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColor : MonoBehaviour, IEffect
{
    public float m_delay;
    public Color m_valueStart;
    public Color m_valueEnd;
    public float m_durationK;
    SpriteRenderer m_renderer;

    public void Init()
    {
    }

    public void Run(float force)
    {
        if (m_renderer == null)
        {
            m_renderer = GetComponent<SpriteRenderer>();
        }
        gameObject.SetActive(true);
        StartCoroutine(_Run(force));
    }

    IEnumerator _Run(float force)
    {
        yield return new WaitForSeconds(m_delay);
        Color value0 = m_valueStart;
        Color valueD = m_valueEnd - m_valueStart;
        float duration = force * m_durationK;
        float duration_1 = 1 / duration;
        float t0 = Time.time;
        float t1 = t0 + duration;
        float k;
        while (t1 > Time.time)
        {
            k = (Time.time - t0) * duration_1;
            m_renderer.color = value0 + valueD * k;
            yield return null;
        }
        //gameObject.SetActive(false);
    }
}
