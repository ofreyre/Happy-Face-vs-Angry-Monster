using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class WeaponsCollector : MonoBehaviour {

    public Transform[] m_weapons;
    public string m_collectSFX;
    int[] m_ammounts;
    private int m_amount;
    int m_currentWeapon;
    int m_upgradeAmount;
    int m_weaponI;

    void Start() {
        m_currentWeapon = 0;
        m_ammounts = new int[m_weapons.Length];
        m_ammounts[0] = -1;
        EventManager.instance.WeaponCollected += Collect;
        EventManager.instance.ChargeWeapon += Collect;
        UserVector.NewVectorLapse += Burn;
    }

    int GetShooterPointsIndex(INVENTORYITEM_TYPE type) {
        for (int i = 0, n = m_weapons.Length; i < n; i++) {
            if (m_weapons[i].GetComponent<WeaponType>().m_type == type) {
                return i;
            }
        }
        return -1;
    }

    Transform GetWeapon(INVENTORYITEM_TYPE type)
    {
        int i = GetShooterPointsIndex(type);
        if (i > -1) {
            return m_weapons[i];
        }
        return null;
    }

    void Collect(CollectableWeapon collectable) {
        Collect(collectable.m_type, collectable.m_amount);
    }

    void Collect(INVENTORYITEM_TYPE type, int amount, bool force = false)
    {
        AudioManager.instance.Play(m_collectSFX);
        int i = GetShooterPointsIndex(type);
        if (i > -1)
        {
            if (force || i > m_currentWeapon)
            {
                SetWeapon(i, amount);
            }
            else {
                AddAmount(i, amount);
            }
        }
    }

    void SetWeapon(int i, int amount)
    {
        m_weaponI = i;
        if (amount > 0)
        {
            AddAmount(m_weaponI, amount);
        }
        m_amount = m_ammounts[m_weaponI];
        EventManager.instance.DispatchWeaponReady(m_weapons[m_weaponI]);
    }

    private void AddAmount(int i, int amount)
    {
        m_ammounts[i] += amount + m_upgradeAmount;
    }

    void Burn(Vector3 direction) {
        if (m_amount > 0)
        {
            m_amount--;
            if (m_amount < 1) {
                Exhausted();
            }
        }
    }

    void Exhausted() {
        if (m_amount > -1)
        {
            m_ammounts[m_weaponI] = 0;
        }

        int amount;
        for (int i = m_ammounts.Length - 1; i > -1; i--) {
            amount = m_ammounts[i];
            if (amount > 0 || amount == -1) {
                SetWeapon(i, 0);
                return;
            }
        }
    }

    public void ApplyUpgrades(int amount) {
        m_upgradeAmount = amount;
    }
}
