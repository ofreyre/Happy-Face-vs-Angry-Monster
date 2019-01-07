using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisactivateChildren : MonoBehaviour, IEffect
{
    public float m_delay;

    public void Init()
    {
    }

    public void Run(float force)
    {
        Invoke("Disactivate", m_delay);
    }

    public void Disactivate() {
        foreach (Transform t in transform) {
            t.gameObject.SetActive(false);
        }
    }
}
