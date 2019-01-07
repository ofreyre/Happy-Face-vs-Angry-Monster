using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class OnCollisionGiveStar : MonoBehaviour {
    public string m_colliderName;
    public string m_milestoneSFX;

    void Start() { }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == m_colliderName) {
            AudioManager.instance.Play(m_milestoneSFX);
            Stars.instance.Add(1, transform.position);
            enabled = false;
        }
    }
}
