using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyOnCollision : MonoBehaviour, IEffect
{
    public float m_destroyDelay;

    // Use this for initialization
    public void Init() {
    }

    public void Run(float force) {
        gameObject.SetActive(true);
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, m_destroyDelay);
	}
}
