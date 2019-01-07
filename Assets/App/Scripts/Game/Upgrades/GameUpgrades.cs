using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUpgrades: MonoBehaviour
{
    public UpgradesCatalog catalog;
    public DBupgradesAmmo ammo;
    public DBupgradesBomb bomb;
    public DBupgradesCharacter character;
    public DBupgradesShield shield;
    public DBupgradesStamina stamina;
    public DBupgradesWeapon weapon;

    void Start() {
    }

    void FillFromDB() {
    }




    public float ammoAmount
    {
        set
        {
            ammo.amount = value;
        }

        get
        {
            return ammo.amount;
        }
    }

    public float ammoStamina
    {
        set
        {
            ammo.stamina = value;
        }

        get
        {
            return ammo.stamina;
        }
    }

    public float ammoDamage
    {
        set
        {
            ammo.damage = value;
        }

        get
        {
            return ammo.damage;
        }
    }

    public float ammoShield
    {
        set
        {
            ammo.shield = value;
        }

        get
        {
            return ammo.shield;
        }
    }

    public float ammoDuration
    {
        set
        {
            ammo.duration = value;
        }

        get
        {
            return ammo.duration;
        }
    }

    public float ammoSpeed
    {
        set
        {
            ammo.speed = value;
        }

        get
        {
            return ammo.speed;
        }
    }


    public float bombDamage
    {
        set
        {
            bomb.damage = value;
        }

        get
        {
            return bomb.damage;
        }
    }

    public float bombRadius
    {
        set
        {
            bomb.radius = value;
        }

        get
        {
            return bomb.radius;
        }
    }

    public float bombDuration
    {
        set
        {
            bomb.duration = value;
        }

        get
        {
            return bomb.duration;
        }
    }

    public float bombLapse
    {
        set
        {
            bomb.lapse = value;
        }

        get
        {
            return bomb.lapse;
        }
    }





    public float characterStamina
    {
        set
        {
            character.stamina = value;
        }

        get
        {
            return character.stamina;
        }
    }

    public float characterDamage
    {
        set
        {
            character.damage = value;
        }

        get
        {
            return character.damage;
        }
    }

    public float characterShield
    {
        set
        {
            character.shield = value;
        }

        get
        {
            return character.shield;
        }
    }

    public float shieldStamina
    {
        set
        {
            shield.stamina = value;
        }

        get
        {
            return shield.stamina;
        }
    }

    public float shieldDamage
    {
        set
        {
            shield.damage = value;
        }

        get
        {
            return shield.damage;
        }
    }

    public float shieldShield
    {
        set
        {
            shield.shield = value;
        }

        get
        {
            return shield.shield;
        }
    }

    public float shieldRadius
    {
        set
        {
            shield.radius = value;
        }

        get
        {
            return shield.radius;
        }
    }

    public int StaminaAmount
    {
        set
        {
            stamina.amount = value;
        }

        get
        {
            return stamina.amount;
        }
    }

    public int WeaponAmount
    {
        set
        {
            weapon.amount = value;
        }

        get
        {
            return weapon.amount;
        }
    }
}
