using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectDelegateOnCollision : MonoBehaviour, IEffect
{
    public Action<Collider2D> m_delegate;
    public float m_callDelay;

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
        StartCoroutine(Call(col));
    }

    IEnumerator Call(Collider2D col)
    {
        yield return new WaitForSeconds(m_callDelay);
        m_delegate(col);
    }
}
