using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisactivateOnCollision : MonoBehaviour, IEffect
{
    public float m_disactivateDelay;

    // Use this for initialization
    public void Init()
    {
    }

    public void Run(float force)
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        Invoke("Disactivate", m_disactivateDelay);
    }

    void Disactivate() {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
    }
}
