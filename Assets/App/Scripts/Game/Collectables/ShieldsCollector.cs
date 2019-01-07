using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class ShieldsCollector : MonoBehaviour {
    
    public ForceField[] m_forceFields;
    public string m_collectSFX;
    int m_forceFieldI = -1;
    float m_staminaUpgrade;
    ForceField local_forceField;

    // Use this for initialization
    void Start ()
    {
        EventManager.instance.ShieldCollected += Collect;
        EventManager.instance.ChargeShield += Collect;
        EventManager.instance.ShieldExhausted += Exhausted;
    }

    int GetForceFieldIndex(GHOST_TYPE type)
    {
        for (int i = 0, n = m_forceFields.Length; i < n; i++)
        {
            local_forceField = m_forceFields[i];
            if (local_forceField.m_ghostType == type)
            {
                return i;
            }
        }
        return -1;
    }

    int GetForceFieldIndex(INVENTORYITEM_TYPE type)
    {
        for (int i = 0, n = m_forceFields.Length; i < n; i++)
        {
            local_forceField = m_forceFields[i];
            if (local_forceField.m_itemType == type)
            {
                return i;
            }
        }
        return -1;
    }

    void SetShield(int i, float amount, bool force = false)
    {
        if (m_forceFieldI > -1)
        {
            local_forceField = m_forceFields[m_forceFieldI];
            if (force || local_forceField.m_ghostType != GHOST_TYPE.all)
            {
                local_forceField.gameObject.SetActive(false);
                m_forceFieldI = i;
            }
        }
        else
        {
            m_forceFieldI = i;
        }
        local_forceField = m_forceFields[m_forceFieldI];
        local_forceField.stamina += amount + m_staminaUpgrade;
        if (!local_forceField.gameObject.activeSelf)
        {
            local_forceField.gameObject.SetActive(true);
        }
    }

    void SetShield(int i)
    {
        m_forceFieldI = i;
        local_forceField = m_forceFields[m_forceFieldI];
        if (!local_forceField.gameObject.activeSelf)
        {
            local_forceField.gameObject.SetActive(true);
        }
    }

    void SetShield(INVENTORYITEM_TYPE type, float amount, bool force = false)
    {
        int i = GetForceFieldIndex(type);
        if (i > -1)
        {
            SetShield(i, amount, force);
        }
    }

    void Collect(CollectableShield collectable)
    {
        AudioManager.instance.Play(m_collectSFX);
        SetShield(collectable.m_itemType, collectable.m_amount);
    }
    
    void Collect(INVENTORYITEM_TYPE type, float amount, bool force = false)
    {
        AudioManager.instance.Play(m_collectSFX);
        SetShield(type, amount, force);
    }

    void Exhausted()
    {
        if (m_forceFieldI > -1)
        {
            m_forceFields[m_forceFieldI].gameObject.SetActive(false);
        }
        for (int i = m_forceFields.Length - 1; i > -1; i--)
        {
            if (m_forceFields[i].m_stamina > 0) {
                SetShield(i);
                return;
            }
        }
        m_forceFieldI = -1;
    }

    public void ApplyUpgrades(float stamina, float damage, float shield, float radius)
    {
        Transform ts = m_forceFields[0].transform;
        float size0 = ts.GetComponent<Renderer>().bounds.extents.x * 2;
        float size = size0 + radius * 2;
        float scale = size / size0 * ts.localScale.x;
        Vector3 scale3 = new Vector3(scale, scale, 1);
        m_staminaUpgrade += stamina;
        for (int i = 0, n = m_forceFields.Length; i < n; i++)
        {
            local_forceField = m_forceFields[i];
            local_forceField.m_damage += damage;
            local_forceField.m_shield += shield;
            local_forceField.transform.localScale = scale3;
        }
    }
}
