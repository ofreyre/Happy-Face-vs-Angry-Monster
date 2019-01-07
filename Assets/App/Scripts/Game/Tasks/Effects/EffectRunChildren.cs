using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRunChildren : MonoBehaviour, IEffect
{
    public float m_delayK;

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
        yield return new WaitForSeconds(force * m_delayK);
        IEffect[] effects;
        foreach (Transform child in transform)
        {
            effects = child.GetComponents<IEffect>();
            //Debug.Log("EffectRunChildren " + child.name + " " + effects.Length);
            for (int i = 0, n = effects.Length; i < n; i++)
            {
                effects[i].Run(force);
            }
        }
    }
}
