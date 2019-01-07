using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager instance;
    public delegate void DelegateAmmoCollected(CollectableAmmo collectable);
    public DelegateAmmoCollected AmmoCollected;
    public delegate void DelegateBombCollected(CollectableBomb collectable);
    public DelegateBombCollected BombCollected;
    public delegate void DelegateStaminaCollected(CollectableStamina collectable);
    public DelegateStaminaCollected StaminaCollected;
    public delegate void DelegateShieldCollected(CollectableShield collectable);
    public DelegateShieldCollected ShieldCollected;
    public delegate void DelegateShieldExhausted();
    public DelegateShieldExhausted ShieldExhausted;
    public delegate void DelegateWeaponCollected(CollectableWeapon collectable);
    public DelegateWeaponCollected WeaponCollected;
    public delegate void DelegateWeaponReady(Transform weapon);
    public DelegateWeaponReady WeaponReady;
    public delegate void DelegateAmmoExhausted();
    public DelegateAmmoExhausted AmmoExhausted;
    public delegate void DelegateLose();
    public DelegateLose Lose;
    public delegate void DelegateEnd();
    public DelegateLose End;
    public delegate void DelegateWin();
    public DelegateWin Win;

    public delegate void DelegateItemSelected(ItemUI item);
    public DelegateItemSelected ItemSelected;

    public delegate void DelegateItemConsumed(DBinventoryItem item);
    public DelegateItemConsumed ItemConsumed;

    public delegate void DelegateChargeShield(INVENTORYITEM_TYPE type, float amount, bool force);
    public DelegateChargeShield ChargeShield;
    public delegate void DelegateChargeAmmo(INVENTORYITEM_TYPE type, int amount, bool force);
    public DelegateChargeAmmo ChargeAmmo;
    public delegate void DelegateChargeBomb(INVENTORYITEM_TYPE type, float power, float radius, float duration);
    public DelegateChargeBomb ChargeBomb;
    public delegate void DelegateChargeStamina(DBinventoryItem item);
    public DelegateChargeStamina ChargeStamina;
    public delegate void DelegateChargeWeapon(INVENTORYITEM_TYPE type, int amount, bool force);
    public DelegateChargeWeapon ChargeWeapon;

    void Awake () {
        instance = this;
    }

    public void DispatchAmmoCollected(CollectableAmmo collectable) {
        if (AmmoCollected != null) {
            AmmoCollected(collectable);
        }
    }

    public void DispatchBombCollected(CollectableBomb collectable)
    {
        if (BombCollected != null)
        {
            BombCollected(collectable);
        }
    }

    public void DispatchStaminaCollected(CollectableStamina collectable)
    {
        if (StaminaCollected != null)
        {
            StaminaCollected(collectable);
        }
    }

    public void DispatchShieldCollected(CollectableShield collectable)
    {
        if (ShieldCollected != null)
        {
            ShieldCollected(collectable);
        }
    }

    public void DispatchShieldExhausted()
    {
        if (ShieldExhausted != null)
        {
            ShieldExhausted();
        }
    }

    public void DispatchWeaponCollected(CollectableWeapon collectable)
    {
        if (WeaponCollected != null)
        {
            WeaponCollected(collectable);
        }
    }

    public void DispatchWeaponReady(Transform weapon)
    {
        if (WeaponReady != null)
        {
            WeaponReady(weapon);
        }
    }

    public void DispatchAmmoExhausted()
    {
        if (AmmoExhausted != null)
        {
            AmmoExhausted();
        }
    }

    public void DispatchLose()
    {
        if (Lose != null)
        {
            Lose();
        }
    }

    public void DispatchEnd()
    {
        if (End != null)
        {
            End();
        }
    }

    public void DispatchWin()
    {
        if (Win != null)
        {
            Win();
        }
    }

    public void DispatchItemSelected(ItemUI item)
    {
        if (ItemSelected != null)
        {
            ItemSelected(item);
        }
    }

    public void DispatchItemConsumed(DBinventoryItem item)
    {
        if (ItemConsumed != null)
        {
            ItemConsumed(item);
        }
    }

    public void DispatchChargeShield(INVENTORYITEM_TYPE type, float amount, bool force)
    {
        if (ChargeShield != null)
        {
            ChargeShield(type, amount, force);
        }
    }

    public void DispatchChargeAmmo(INVENTORYITEM_TYPE type, int amount, bool force)
    {
        if (ChargeAmmo != null)
        {
            ChargeAmmo(type, amount, force);
        }
    }

    public void DispatchChargeBomb(INVENTORYITEM_TYPE type, float power, float radius, float duration)
    {
        if (ChargeBomb != null)
        {
            ChargeBomb(type, power, radius, duration);
        }
    }

    public void DispatchChargeStamina(DBinventoryItem item)
    {
        if (ChargeStamina != null)
        {
            ChargeStamina(item);
        }
    }

    public void DispatchChargeWeapon(INVENTORYITEM_TYPE type, int amount, bool force)
    {
        if (ChargeWeapon != null)
        {
            ChargeWeapon(type, amount, force);
        }
    }
}
