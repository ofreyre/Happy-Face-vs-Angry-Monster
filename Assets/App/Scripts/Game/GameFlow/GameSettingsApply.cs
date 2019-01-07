using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsApply : MonoBehaviour {
    public GameStats m_gameStats;
    public UpgradesCatalog m_upgrades;
    public GlobalFlow m_globalFlow;
    Dictionary<INVENTORYITEM_CATEGORY, Dictionary<UPGRADE_TYPE, float>> m_upgradeAmounts;

    private void Awake()
    {
        //ApplyUserInput();
        ApplyUpgrades();
    }

    /*public void ApplyUserInput()
    {
        USERVECTOR_TYPE type = DBmanager.UserControls;
        Debug.Log(type);
        Transform userinputs = GameObject.Find("UserInputs").transform;
        foreach (Transform ts in userinputs) {
            Debug.Log(ts.name+" "+ (ts.GetComponent<UserVector>().type == type));
            ts.gameObject.SetActive(ts.GetComponent<UserVector>().type == type);
        }
    }*/

    void ApplyUpgrades()
    {
        FillUpgradeAmounts();
        ApplyUpgradesCharacter();
        ApplyUpgradesAmmo();
        ApplyUpgradesWeapon();
        ApplyUpgradesShield();
        ApplyUpgradesBomb();
    }

    void FillUpgradeAmounts()
    {
        DBupgrades dbUpgrades = DBmanager.Upgrades;
        UpgradesCategoryCatalog[] categoryCatalog = m_upgrades.upgrades;
        UpgradesCategoryCatalog categoryUpgrades;
        INVENTORYITEM_CATEGORY category;
        UpgradeItem[] upgradesCategory;
        UpgradeItem upgrade;
        m_upgradeAmounts = new Dictionary<INVENTORYITEM_CATEGORY, Dictionary<UPGRADE_TYPE, float>>();
        Dictionary<UPGRADE_TYPE, float> upgradeTypeAmount;
        int upgradesCount, upgradeCatalogCount;
        for (int i = 0, n = categoryCatalog.Length; i < n; i++)
        {
            categoryUpgrades = categoryCatalog[i];
            category = categoryUpgrades.category;
            upgradesCategory = categoryUpgrades.upgrades;
            upgradesCount = dbUpgrades.Categoty2Count(category).count;
            upgradeCatalogCount = upgradesCategory.Length;
            upgradeTypeAmount = new Dictionary<UPGRADE_TYPE, float>();
            m_upgradeAmounts.Add(category, upgradeTypeAmount);
            for (int j = 0; j < upgradeCatalogCount; j++)
            {
                upgrade = upgradesCategory[j];
                if (!upgradeTypeAmount.ContainsKey(upgrade.type))
                {
                    upgradeTypeAmount.Add(upgrade.type, 0);
                }
                if (j < upgradesCount)
                {
                    upgradeTypeAmount[upgrade.type] += upgrade.amount;
                }
            }
        }
    }

    void ApplyUpgradesCharacter()
    {
        Stamina stamina = GameObject.Find("Stamina").GetComponent<Stamina>();
        stamina.m_valueMax = m_gameStats.staminaMax + GetUpgrade(INVENTORYITEM_CATEGORY.character, UPGRADE_TYPE.stamina);
        stamina.m_value = stamina.ValueMax;

        GameObject.Find("Pacman").GetComponent<Victim>().ApplyUpgrades(
            m_gameStats.shield + GetUpgrade(INVENTORYITEM_CATEGORY.character, UPGRADE_TYPE.shield),
            stamina.m_valueMax,
            m_gameStats.damage + GetUpgrade(INVENTORYITEM_CATEGORY.character, UPGRADE_TYPE.damage),
            GetUpgrade(INVENTORYITEM_CATEGORY.stamina, UPGRADE_TYPE.amount)
        );
    }

    void ApplyUpgradesAmmo()
    {
        //Debug.Log("GameSettingsApply.ApplyUpgradesAmmo");
        GameObject.Find("Shooters").GetComponent<ShootersManager>().ApplyUpgrades(
            GetUpgrade(INVENTORYITEM_CATEGORY.weapon, UPGRADE_TYPE.force),
            GetUpgrade(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.duration),
            (int)GetUpgrade(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.amount),
            GetUpgrade(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.shield),
            GetUpgrade(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.stamina),
            GetUpgrade(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.damage)
        );
    }

    void ApplyUpgradesBomb()
    {
        GameObject.Find("BombCollector").GetComponent<BombCollector>().ApplyUpgrades(
            GetUpgrade(INVENTORYITEM_CATEGORY.bomb, UPGRADE_TYPE.damage),
            GetUpgrade(INVENTORYITEM_CATEGORY.bomb, UPGRADE_TYPE.raduis),
            GetUpgrade(INVENTORYITEM_CATEGORY.bomb, UPGRADE_TYPE.duration),
            GetUpgrade(INVENTORYITEM_CATEGORY.bomb, UPGRADE_TYPE.lapse)
        );
    }

    void ApplyUpgradesShield()
    {
        GameObject.Find("ShieldsCollector").GetComponent< ShieldsCollector >().ApplyUpgrades(
            GetUpgrade(INVENTORYITEM_CATEGORY.shield, UPGRADE_TYPE.stamina),
            GetUpgrade(INVENTORYITEM_CATEGORY.shield, UPGRADE_TYPE.damage),
            GetUpgrade(INVENTORYITEM_CATEGORY.shield, UPGRADE_TYPE.shield),
            GetUpgrade(INVENTORYITEM_CATEGORY.shield, UPGRADE_TYPE.raduis)
        );
    }

    void ApplyUpgradesWeapon()
    {
        USERVECTOR_TYPE type = DBmanager.UserControls;
        float lapse = GetUpgrade(INVENTORYITEM_CATEGORY.weapon, UPGRADE_TYPE.lapse);
        Transform userinputs = GameObject.Find("UserInputs").transform;
        foreach (Transform ts in userinputs)
        {
            ts.GetComponent<UserVector>().m_lapse += lapse;
            ts.gameObject.SetActive(ts.GetComponent<UserVector>().type == type);
        }
        
        GameObject.Find("WeaponsCollector").GetComponent<WeaponsCollector>().ApplyUpgrades(
            (int)GetUpgrade(INVENTORYITEM_CATEGORY.weapon, UPGRADE_TYPE.amount)
        );
    }

    public float GetUpgrade(INVENTORYITEM_CATEGORY category, UPGRADE_TYPE type) {
        return m_upgradeAmounts[category][type];
    }
}
