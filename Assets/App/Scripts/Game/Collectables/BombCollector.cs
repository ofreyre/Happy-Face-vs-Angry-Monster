using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class BombCollector : MonoBehaviour
{
    public Bomb[] m_bombs;
    public string m_collectSFX;
    Bomb local_bomb;

    void Start()
    {
        EventManager.instance.ChargeBomb += OnChargeBomb;
    }

    void OnChargeBomb(INVENTORYITEM_TYPE type, float power, float radius, float duration)
    {
        AudioManager.instance.Play(m_collectSFX);
        Bomb bomb = GetBomb(type);
        bomb.Explode(power, radius, duration);
    }

    Bomb GetBomb(INVENTORYITEM_TYPE type) {
        for (int i = 0, n = m_bombs.Length; i < n; i++) {
            local_bomb = m_bombs[i];
            if (local_bomb.m_type == type) {
                return local_bomb;
            }
        }
        return null;
    }

    public void ApplyUpgrades(float damage, float radius, float duration, float lapse)
    {
        for (int i = 0, n = m_bombs.Length; i < n; i++)
        {
            local_bomb = m_bombs[i];
            local_bomb.m_lapse += lapse;
            local_bomb.m_damageUpgrade = damage;
            local_bomb.m_durationUpgrade = duration;
            local_bomb.m_radiusUpgrade = radius;
        }
    }
}
