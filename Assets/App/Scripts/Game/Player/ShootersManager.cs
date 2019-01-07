using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using AudioManagement;

public class ShootersManager : MonoBehaviour {
    [Serializable]
    public struct AmmoTypeIndex {
        public INVENTORYITEM_TYPE type;
        public int m_shooterI;
    }

    public Shooter[] m_shooters;
    public AmmoTypeIndex[] m_ammoType2shooterIndex;
    public Transform m_defaultWeapon;
    public string m_collectSFX;
    int m_shooterI = -1;
    int m_upgradeAmount;
    Transform m_weapon;

    // Use this for initialization
    void Start () {
        EventManager.instance.AmmoCollected += OnAmmoCollected;
        EventManager.instance.AmmoExhausted += OnAmmoExhausted;
        EventManager.instance.WeaponReady += SetShootPoints;
        EventManager.instance.ChargeAmmo += OnChargeAmmo;
        m_weapon = m_defaultWeapon;
        SetShooter(0);
    }

    void SetShootPoints(Transform weapon)
    {
        m_weapon = weapon;
        m_shooters[m_shooterI].SetWeapon(weapon);
    }

    //amount == -2 == exhausted
    public void SetShooter(int shooterI, int amount = -1, bool force = false)
    {
        if (force || shooterI > m_shooterI)
        {
            if (m_shooterI > -1)
            {
                m_shooters[m_shooterI].Disactivate();
            }
            m_shooterI = shooterI;
            Ammo.instance.type = shooterI;
            Shooter shooter = m_shooters[m_shooterI];
            if (amount < 0)
            {
                shooter.Activate(amount, m_weapon);
            }
            else
            {
                shooter.Activate(amount + m_upgradeAmount, m_weapon);
            }
        }
        else if (amount > 0)
        {
            m_shooters[shooterI].AddAmount(amount + m_upgradeAmount);
        }
    }

    void OnAmmoCollected(CollectableAmmo collectable)
    {
        AudioManager.instance.Play(m_collectSFX);
        SetShooter(collectable.m_shooterIndex, collectable.m_amount);
    }

    void OnChargeAmmo(INVENTORYITEM_TYPE type, int amount, bool force) {
        AudioManager.instance.Play(m_collectSFX);
        int i = AmmoType2shooterIndex(type);
        if (i > -1) {
            SetShooter(i, amount, force);
        }
    }

    void OnAmmoExhausted() {
        for (int i = m_shooterI - 1; i > 0; i--) {
            if (m_shooters[i].m_amount > 0) {
                SetShooter(i, -2, true);
                return;
            }
        }
        SetShooter(0, -1, true);
    }

    int AmmoType2shooterIndex(INVENTORYITEM_TYPE type) {
        AmmoTypeIndex m_typeIndex;
        for (int i = 0, n = m_ammoType2shooterIndex.Length; i < n; i++) {
            m_typeIndex = m_ammoType2shooterIndex[i];
            if (m_typeIndex.type == type) {
                return m_typeIndex.m_shooterI;
            }
        }
        return -1;

    }

    public void ApplyUpgrades(float speed, float duration, int amount, float shield, float stamina, float damage)
    {
        m_upgradeAmount = amount;
        for (int i = 0, n = m_shooters.Length; i < n; i++) {
            m_shooters[i].ApplyUpgrades(speed, duration, shield, stamina, damage);
        }
    }
}
