using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoreList : MonoBehaviour {
    public DBcatalog m_catalog;
    public GameObject m_itemPrefab;
    StoreItemUI[] m_items;

    void Start() {
        DBinventoryItem[] items = m_catalog.items;
        Array.Sort(items, DBinventoryItem.CompareDBinventoryItem);
        m_items = new StoreItemUI[items.Length];
        DBinventoryItem item;
        StoreItemUI storeItem;
        DBinventory inventory = DBmanager.GetInventory();
        int money = inventory.coins;
        for (int i = 0, n = items.Length; i < n; i++) {
            item = items[i];
            if (item.visibleInInventory)
            {
                storeItem = Instantiate(m_itemPrefab).GetComponent<StoreItemUI>();
                storeItem.Fill(item, inventory.GetItems(item.type));
                storeItem.Enable(money);
                storeItem.transform.SetParent(transform);
                //storeItem.transform.localScale = Vector3.one;
                storeItem.Fit();
                m_items[i] = storeItem;
            }
        }
    }

    public int AddAmount(StoreItemUI item, int amount)
    {
        item.Amount += amount;
        return item.Amount;
    }

    public void UpdateItemsEnabled(int money)
    {
        StoreItemUI _item;
        for (int i = m_items.Length - 1; i > -1; i--)
        {
            _item = m_items[i];
            if (_item != null)
            {
                _item.Enable(money);
            }
        }
    }
}