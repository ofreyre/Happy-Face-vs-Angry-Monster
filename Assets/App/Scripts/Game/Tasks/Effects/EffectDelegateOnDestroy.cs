using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectDelegateOnDestroy : MonoBehaviour, IEffect
{
    public Action<Transform> m_delegate;

    // Use this for initialization
    public void Init()
    {
    }

    public void Run(float force)
    {
        gameObject.SetActive(true);
    }

    public void OnDestroy() {
        m_delegate(transform);
    }
}
