using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float m_shield;
    public float m_stamina;
    public float m_damage;
    public float m_destructionForce = 1;
    public int m_points = 1;
    public float bounceK = 1;

    public bool Damage(float damage) {
        m_stamina -= damage * (1 - m_shield);
        return m_stamina < 0;
    }

    public float GetDamageResult(float damage) {
        return damage * (1 - m_shield);
    }
}
