using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManagement;
using DBGads;

public class ItemsStore : MonoBehaviour {

    public EventManagerMessages m_eventManager;
    public Text m_moneyUI;
    public StoreList m_list;
    public Button m_btnAdd;
    public string m_consumeSFX;
    ItemUI m_itemUI;

    // Use this for initialization
    void Start () {
        m_moneyUI.text = DBmanager.GetCoins().ToString();
        EventManager.instance.ItemSelected += Selected;
        m_eventManager.Int += UpdateCoinsView;
        WatchVideo.instance.Reward += AddCoins;
    }

    void Selected(ItemUI itemUI)
    {
        if (m_itemUI != itemUI)
        {
            if (m_itemUI != null)
            {
                ((StoreItemUI)m_itemUI).Unselect();
            }
            m_itemUI = itemUI;
            m_btnAdd.interactable = DBmanager.GetCoins() >= m_itemUI.m_item.price;
        }
    }

    public void Purchase() {
        int coins = DBmanager.GetCoins();
        DBinventoryItem item = m_itemUI.m_item;
        if (m_itemUI != null && coins >= item.price)
        {
            AudioManager.instance.Play(m_consumeSFX);
            DBmanager.AddItems(item.type, 1);
            AddCoins(-item.price);
            m_list.AddAmount((StoreItemUI)m_itemUI, 1);
            ((StoreItemUI)m_itemUI).Select();
        }
    }

    void AddCoins(int amount)
    {
        DBmanager.Coins += amount;
        UpdateCoinsView();
    }

    void UpdateCoinsView(int amount = 0)
    {
        int coins = DBmanager.GetCoins();
        m_moneyUI.text = coins.ToString();
        m_list.UpdateItemsEnabled(coins);
    }
}
