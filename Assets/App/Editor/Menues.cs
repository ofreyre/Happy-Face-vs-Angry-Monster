using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Menues
{

    [MenuItem("Assets/Create/Globals/Globals")]
    [MenuItem("Globals/Game stats")]
    public static void Globals_new()
    {
        TasksMenu.CreateAsset<Globals>();
    }

    [MenuItem("Assets/Create/Globals/Flow")]
    [MenuItem("Globals/Flow")]
    public static void GlobalFlow_new()
    {
        TasksMenu.CreateAsset<GlobalFlow>();
    }

    [MenuItem("Assets/Create/Globals/Game stats")]
    [MenuItem("Game assets/Game stats")]
    public static void GameStats_new()
    {
        TasksMenu.CreateAsset<GameStats>();
    }

    [MenuItem("Assets/Create/Globals/Upgrades catalog")]
    [MenuItem("Game assets/Game stats")]
    public static void UpgradesCatalog_new()
    {
        TasksMenu.CreateAsset<UpgradesCatalog>();
    }

    [MenuItem("Assets/Create/Game assets/Items catalog")]
    [MenuItem("Game assets/Items catalog")]
    public static void Catalog_new()
    {
        DBcatalog catalog = TasksMenu.CreateAsset<DBcatalog>();


        INVENTORYITEM_TYPE[] types = UtilsEnum.Enum2Array<INVENTORYITEM_TYPE>();
        catalog.items = new DBinventoryItem[types.Length];
        DBinventoryItem item;
        for (int i = 0, n = types.Length; i < n; i++)
        {
            item = UtilsScriptableObject.CreateAsset<DBinventoryItem>();
            item.type = types[i];
            catalog.items[i] = item;

        }
    }

    [MenuItem("Assets/Create/Game assets/Catalog item")]
    [MenuItem("Game assets/Catalog item")]
    public static void CatalogItem_new()
    {
        TasksMenu.CreateAsset<DBinventoryItem>();
    }

    [MenuItem("Assets/Create/Game assets/Catalog bomb")]
    [MenuItem("Game assets/Catalog bomb")]
    public static void CatalogBomb_new()
    {
        TasksMenu.CreateAsset<DBinventoryBomb>();
    }

    [MenuItem("Assets/Create/Globals/Levels Settings")]
    [MenuItem("Game assets/Levels Settings")]
    public static void LevelsSettings_new()
    {
        TasksMenu.CreateAsset<LevelsSettings>();
    }

    [MenuItem("Assets/Createe/Globals/Spawner Bank")]
    [MenuItem("Globals/Spawner Bank")]
    public static void SpawnerBank_new()
    {
        TasksMenu.CreateAsset<SpawnerBank>();
    }

    [MenuItem("Assets/Createe/Globals/Levels Settings Spawner")]
    [MenuItem("Globals/Levels Settings Spawner")]
    public static void LevelsSettingsSpawner_new()
    {
        TasksMenu.CreateAsset<LevelsSettingsSpawner>();
    }

    [MenuItem("Tools/DB/Levels/Reset levels")]
    private static void ResetLevels()
    {
        DBmanager.ResetLevels();
    }
    [MenuItem("Tools/DB/Levels/Delete levels")]
    private static void DeleteLevels()
    {
        DBmanager.DeleteLevels();
    }

    [MenuItem("Tools/DB/Inventory/Reset inventory")]
    private static void ResetInventory()
    {
        DBmanager.ResetInventory();
    }

    [MenuItem("Tools/DB/Inventory/Delete inventory")]
    private static void DeleteInventory()
    {
        DBmanager.DeleteInventory();
    }

    [MenuItem("Tools/DB/Settings/Reset settings")]
    private static void ResetSettings()
    {
        DBmanager.ResetSettings();
    }

    [MenuItem("Tools/DB/Settings/Delete settings")]
    private static void DeleteSettings()
    {
        DBmanager.DeleteSettings();
    }

    [MenuItem("Tools/DB/Upgrades/Reset")]
    private static void ResetUpgrades()
    {
        DBmanager.ResetUpgrades();
    }

    [MenuItem("Tools/DB/Upgrades/Delete")]
    private static void DeleteUpgrades()
    {
        DBmanager.DeleteUpgrades();
    }

    [MenuItem("Tools/DB/CoinsForRate/Reset")]
    private static void ResetCoinsForRate()
    {
        DBmanager.ResetCoinsForRate();
    }

    [MenuItem("Tools/DB/CoinsForRate/Delete")]
    private static void DeleteCoinsForRate()
    {
        DBmanager.DeleteCoinsForRate();
    }

    [MenuItem("Tools/DB/Reset all")]
    private static void DBresetAll()
    {
        DBmanager.ResetAll();
    }

    [MenuItem("Tools/DB/Delete all")]
    private static void DBdeleteAll()
    {
        DBmanager.DeleteAll();
    }

    [MenuItem("Tools/Scene/Replace gameobjects to angry")]
    public static void ReplaceGameObjects()
    {
        ReplaceGameObjectsToAngry.Display();
    }

    /*[MenuItem("Tools/Scene/Fix ApplySetings")]
    public static void ApplySetings()
    {
        FixApplySettings.Display();
    }*/

    [MenuItem("Tools/Scene/Fill LevelsSettings")]
    public static void Fill_LevelsSettings()
    {
        FillLevelsSettings.Display();
    }

    [MenuItem("Tools/Scene/Fill Scripts targets")]
    public static void Fill_ScriptsTargets()
    {
        FillScriptsTarget.Display();
    }
}
