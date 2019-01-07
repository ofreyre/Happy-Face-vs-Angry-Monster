using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisactivate : MonoBehaviour, IEffect {

    public float m_delayK;
    public GameObject m_target;

    public void Init() {
    }

    public void Run(float force) {
        gameObject.SetActive(true);
        Invoke("_Run", force * m_delayK);
    }

    void _Run()
    {
        if (m_target == null)
        {
            gameObject.SetActive(false);
        }
        else {
            m_target.SetActive(false);
        }
    }
}
