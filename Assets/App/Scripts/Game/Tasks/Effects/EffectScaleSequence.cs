using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScaleSequence : MonoBehaviour, IEffect
{
    public Vector3 m_startValue;
    public float[] m_durations;
    public Vector3[] m_values;
    public bool m_disactivateAtEnd;

    public void Init()
    {
    }

    public void Run(float force)
    {
        gameObject.SetActive(true);
        StartCoroutine(_Run(force));
    }

    IEnumerator _Run(float force)
    {
        transform.localScale = m_startValue;
        Vector3 value0 = m_startValue * force;
        Vector3 valueD;
        float duration;
        float t0, t1;
        for (int i = 0, n = m_values.Length; i < n; i++)
        {
            valueD = m_values[i] * force - value0;
            duration = m_durations[i];
            t0 = Time.time;
            t1 = t0 + duration;
            duration = 1 / duration;
            float k;
            while (t1 > Time.time)
            {
                k = (Time.time - t0) * duration;
                transform.localScale = value0 + valueD * k;
                yield return null;
            }
            value0 = m_values[i] * force;
        }
        if (m_disactivateAtEnd)
        {
            gameObject.SetActive(false);
        }
    }
}
