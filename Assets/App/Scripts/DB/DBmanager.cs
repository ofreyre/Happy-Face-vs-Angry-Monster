using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class DBmanager
{
#region Reusable
    public static void Save<T>(T data, string relativePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath + "/" + relativePath);
        bf.Serialize(file, data);
        file.Close();
    }

    public static T Load<T>(string relativePath) {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + relativePath, FileMode.Open);
        T data = (T)bf.Deserialize(file);
        file.Close();
        return data;
    }
    #endregion


#region Levels
    public static void ResetLevels()
    {
        Globals globals = Resources.Load<Globals>("Globals");
        int levelsCount = globals.LevelsCount;
        bool[] unlocked = new bool[levelsCount];
        int[] scores = new int[levelsCount];
        int[] stars = new int[levelsCount];
                
        //Unlock first level of each stage
        for (int i = 0; i < levelsCount; i+= globals.levelsPerStage) {
            unlocked[i] = true;
        }

       //Unlock all
       /*for (int i = 0; i < levelsCount; i++)
        {
            unlocked[i] = true;
            stars[i] = 3;
        }*/

        DBlevels levels = new DBlevels(unlocked, scores, stars);

        Save(levels, "levels");
    }

    public static DBlevels GetLevels()
    {
        if (File.Exists(Application.persistentDataPath + "/levels"))
        {
            return Load<DBlevels>("levels");
        }
        else
        {
            ResetLevels();
            return Load<DBlevels>("levels");
        }
    }

    public static void DeleteLevels()
    {
        DeleteFile(Application.persistentDataPath + "/levels");
    }

    public static void UnlockLevel(int level)
    {
        DBlevels levels = GetLevels();
        levels.unlocked[level] = true;
        Save(levels, "levels");
    }

    public static void SaveScore(int level, int value)
    {
        DBlevels levels = GetLevels();
        if (levels.score[level] < value)
        {
            levels.score[level] = value;
        }
        Save(levels, "levels");
    }

    public static void SaveStars(int level, int value)
    {
        DBlevels levels = GetLevels();
        if (levels.stars[level] < value)
        {
            levels.stars[level] = value;
        }
        Save(levels, "levels");
    }

    public static int Stars {
        get
        {
            int[] levelsStars = GetLevels().stars;
            int levelsCount = Resources.Load<Globals>("Globals").LevelsCount;
            int starsTotal = 0;
            int stars;
            for (int i = 0; i < levelsCount; i++) {
                stars = levelsStars[i];
                if (stars == 0)
                {
                    //return starsTotal;
                }
                else {
                    starsTotal += stars;
                }
            }
            return starsTotal;
        }
    }

    public static void SaveLevel(int level, int score, int stars)
    {
        DBlevels levels = GetLevels();
        if (levels.score[level] < score)
        {
            levels.score[level] = score;
        }

        if (levels.stars[level] < stars)
        {
            levels.stars[level] = stars;
            if (stars > 0 && level < 98)
            {
                levels.unlocked[level + 1] = true;
            }
        }
        Save(levels, "levels");
    }

    public static int GetUnlockLevelLast(int stage)
    {
        Globals globals = Resources.Load<Globals>("Globals");
        bool[] unlocked = GetLevels().unlocked;
        int i = stage * globals.levelsPerStage;
        for (int n = i + globals.levelsPerStage; i < n; i++)
        {
            if (!unlocked[i])
            {
                break;
            }
        }
        return i - 1;
    }
    #endregion



    #region Inventory
    public static void ResetInventory()
    {
        DBinventory inventory = new DBinventory(0);
        //DBinventory inventory = new DBinventory(9999999);
        Save(inventory, "inventory");
    }

    public static DBinventory GetInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/inventory"))
        {
            return Load<DBinventory>("inventory");
        }
        else
        {
            ResetInventory();
            return Load<DBinventory>("inventory");
        }
    }

    public static void DeleteInventory()
    {
        DeleteFile(Application.persistentDataPath + "/inventory");
    }

    public static int GetCoins()
    {
        DBinventory inventory = GetInventory();
        return inventory.coins;
    }

    public static int Coins {
        get {
            return GetInventory().coins;
        }

        set {
            DBinventory inventory = GetInventory();
            inventory.Coins = value;
            Save(inventory, "inventory");
        }
    }

    public static int AddItems(INVENTORYITEM_TYPE type, int amount)
    {
        DBinventory inventory = GetInventory();
        int i = inventory.AddItems(type, amount);

        Save(inventory, "inventory");
        return i;
    }
    #endregion



    #region Settings
    public static void ResetSettings()
    {
        DBsettings settings = new DBsettings(USERVECTOR_TYPE.DragRelativeToTarget, true, true);
        Save(settings, "settings");
    }

    public static DBsettings GetSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/settings"))
        {
            return Load<DBsettings>("settings");
        }
        else
        {
            ResetSettings();
            return Load<DBsettings>("settings");
        }
    }

    public static void DeleteSettings()
    {
        DeleteFile(Application.persistentDataPath + "/settings");
    }

    public static USERVECTOR_TYPE UserControls {
        get {
            return GetSettings().userControls;
        }

        set
        {
            DBsettings settings = GetSettings();
            settings.userControls = value;

            Save(settings, "settings");
        }
    }

    public static bool AudioOn{
        get {
            DBsettings settings = GetSettings();
            return settings.audioOn;
        }

        set {
            DBsettings settings = GetSettings();
            settings.audioOn = value;
            Save(settings, "settings");
        }
    }
    
    public static void SetShowAdds(bool showAdds)
    {
        DBsettings settings = GetSettings();
        settings.showAdds = showAdds;

        Save(settings, "settings");
    }

    public static USERVECTOR_TYPE GetUserControls()
    {
        DBsettings settings = GetSettings();
        return settings.userControls;
    }

    public static bool GetShowAds()
    {
        DBsettings settings = GetSettings();
        return settings.showAdds;
    }



    static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    #endregion



    #region Upgrades
    public static void ResetUpgrades()
    {
        UpgradesCatalog catalog = Resources.Load<UpgradesCatalog>("UpgradesCatalog");
        UpgradesCategoryCatalog[] catalogUpgrades = catalog.upgrades;
        UpgradesCategoryCatalog upgrade;
        UpgradeCategoryCount[] dbupgrades = new UpgradeCategoryCount[catalogUpgrades.Length];
        for (int i = 0, n = catalogUpgrades.Length; i < n; i++) {
            upgrade = catalogUpgrades[i];
            dbupgrades[i] = new UpgradeCategoryCount(upgrade.category, 0);
        }

        DBupgrades upgrades = new DBupgrades();
        upgrades.categoryCounts = dbupgrades;

        Save(upgrades, "upgrades");
    }

    public static DBupgrades Upgrades
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/upgrades"))
            {
                return Load<DBupgrades>("upgrades");
            }
            else
            {
                ResetUpgrades();
                return Load<DBupgrades>("upgrades");
            }
        }

        set
        {
            Save(value, "upgrades");
        }
    }

    public static void DeleteUpgrades()
    {
        DeleteFile(Application.persistentDataPath + "/upgrades");
    }

    public static void SetUpgrade(INVENTORYITEM_CATEGORY category, int amount)
    {
        DBupgrades upgrades = Upgrades;
        upgrades.SetCount(category, amount);
        Upgrades = upgrades;
    }

    public static void AddUpgrade(INVENTORYITEM_CATEGORY category, int amount)
    {
        DBupgrades upgrades = Upgrades;
        upgrades.AddCount(category, amount);
        Upgrades = upgrades;
    }

    public static void ClearUpgrades()
    {
        DBupgrades upgrades = Upgrades;
        upgrades.Clear();
        Upgrades = upgrades;
    }
    #endregion


    #region CoinsPerRate
    public static void ResetCoinsForRate()
    {

        DBcoinsForRate coinsForRate = new DBcoinsForRate(150);
        Save(coinsForRate, "coinsForRate");
    }

    public static DBcoinsForRate CoinsForRate
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/coinsForRate"))
            {
                return Load<DBcoinsForRate>("coinsForRate");
            }
            else
            {
                ResetCoinsForRate();
                return Load<DBcoinsForRate>("coinsForRate");
            }
        }

        set
        {
            Save(value, "coinsForRate");
        }
    }

    public static void DeleteCoinsForRate()
    {
        DeleteFile(Application.persistentDataPath + "/coinsForRate");
    }

    public static void SetCoinsForRate(int amount)
    {
        DBcoinsForRate coinsForRate = CoinsForRate;
        coinsForRate.amount = amount;
        CoinsForRate = coinsForRate;
    }
    #endregion





    public static void ResetAll() {
        ResetLevels();
        ResetInventory();
        ResetUpgrades();
        ResetSettings();
        ResetCoinsForRate();
    }

    public static void DeleteAll()
    {
        DeleteLevels();
        DeleteInventory();
        DeleteUpgrades();
        DeleteSettings();
        DeleteCoinsForRate();
    }
}
