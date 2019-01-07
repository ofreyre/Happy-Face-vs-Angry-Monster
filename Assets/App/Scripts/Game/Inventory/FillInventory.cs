using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInventory : MonoBehaviour
{
    public DBcatalog m_catalog;
    public Transform m_list;
    public GameObject m_itemPrefab;
    public GameObject m_inventory;
    int m_itemsC;
    DBinventoryItem local_item;
    ItemUI local_itemUI;

    // Use this for initialization
    void Start () {
        int[] itemsAmount = DBmanager.GetInventory().items;
        int itemAmount;
        
        for (int i = 0, n = itemsAmount.Length; i < n; i++) {
            itemAmount = itemsAmount[i];
            if (itemAmount > 0) {
                local_item = m_catalog.GetItem((INVENTORYITEM_TYPE)i);
                if (local_item.visibleInInventory)
                {
                    local_itemUI = Instantiate(m_itemPrefab).GetComponent<ItemUI>();
                    local_itemUI.Fill(local_item, itemAmount);
                    local_itemUI.Enable(0);
                    local_itemUI.transform.SetParent(m_list);
                    local_itemUI.Fit();
                    local_itemUI.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    m_itemsC++;
                }
            }
        }
        if (m_itemsC == 0)
        {
            EventManager.instance.DispatchItemSelected(null);
        }
        else
        {
            EventManager.instance.ItemSelected += Add;
        }

    }

    public void Add(ItemUI item)
    {
        local_item = item.m_item;
        int amount = DBmanager.AddItems(local_item.type, -1);
        if (amount > 0)
        {
            item.Amount = amount;
        }
        else
        {
            Destroy(item.gameObject);
            m_itemsC--;
        }
        if (m_itemsC < 1)
        {
            EventManager.instance.ItemSelected -= Add;
            EventManager.instance.DispatchItemSelected(null);
        }
        EventManager.instance.DispatchItemConsumed(local_item);
    }

    public void Display(bool display) {
        if (!display)
        {
            m_inventory.SetActive(false);
        }
        else
        {
            if (m_itemsC < 1)
            {
                EventManager.instance.ItemSelected -= Add;
                EventManager.instance.DispatchItemSelected(null);
                m_inventory.SetActive(false);
            }
            else
            {
                m_inventory.SetActive(true);
            }
        }
    }
}
