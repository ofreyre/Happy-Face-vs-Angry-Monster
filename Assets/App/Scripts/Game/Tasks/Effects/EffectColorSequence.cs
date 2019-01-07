
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColorSequence : MonoBehaviour, IEffect {
    public Color m_startColor;
    public float[] m_durations;
    public Color[] m_colors;
    public bool m_disactivateAtEnd;

    public void Init() {
    }

    public void Run(float force) {
        gameObject.SetActive(true);
    }

    IEnumerator _Run(float force) {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = m_startColor;
        Color value0 = m_startColor;
        Color valueD;
        float duration;
        float t0, t1;
        for (int i = 0, n = m_colors.Length; i < n; i++) {
            valueD = m_colors[i] - value0;
            duration = m_durations[i];
            t0 = Time.time;
            t1 = t0 + duration;
            duration = 1 / duration;
            float k;
            while (t1 > Time.time)
            {
                k = (Time.time - t0) * duration;
                renderer.color = value0 + valueD * k;
                yield return null;
            }
            value0 = m_colors[i];
        }
        if (m_disactivateAtEnd)
        {
            gameObject.SetActive(false);
        }
    }
}
