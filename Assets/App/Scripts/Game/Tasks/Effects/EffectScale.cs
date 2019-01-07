using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScale : MonoBehaviour, IEffect
{

    public Vector3 m_scaleStart = Vector3.zero;
    public Vector3 m_scaleEnd = Vector3.one;
    public float m_durationK;
    public float m_scaleK;

    public void Init()
    {

    }

    public void Run(float force)
    {
        gameObject.SetActive(true);
        StartCoroutine(_Run(force));
    }

    IEnumerator _Run(float force) {
        Vector3 scale0 = m_scaleStart * force * m_scaleK;
        Vector3 scaleD = (m_scaleEnd - m_scaleStart) * force * m_scaleK;
        float duration = force * m_durationK;
        float duration_1 = 1 / duration;
        float t0 = Time.time;
        float t1 = t0 + duration;
        float k;
        while (t1 > Time.time) {
            k = (Time.time - t0) * duration_1;
            transform.localScale = scale0 + scaleD * k;
            yield return null;
        }
    }
}
