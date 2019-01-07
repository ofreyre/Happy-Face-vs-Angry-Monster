//Reference
//Configurando la Apple App Store y la Mac App Store: https://docs.unity3d.com/es/current/Manual/UnityIAPAppleConfiguration.html
//Codeless IAP: https://docs.unity3d.com/Manual/UnityIAPCodelessIAP.html
//Restore Transactions via Codeless IAP: https://forum.unity.com/threads/restore-transactions-via-codeless-iap.479251/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

[Serializable]
public struct PurchaseItem
{
    public string id;
    public int coins;
}

public class Purchaser : MonoBehaviour
{
    public static Purchaser instance;
    public Text m_moneyUI;
    public MessageUI m_messageUI;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_moneyUI.text = DBmanager.GetCoins().ToString();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product != null)
        {
            DBmanager.Coins += (int)product.definition.payout.quantity;
            m_moneyUI.text = DBmanager.Coins.ToString();
            m_messageUI.Success("Congratulations!\nYou have obtained " + product.definition.payout.quantity + ".");
            DBmanager.SetShowAdds(false);
        }
        else
        {
            m_messageUI.Failure("Maybe next time.");
        }
    }

    public void OnPurchaseFailed()
    {
        m_messageUI.Failure("Maybe next time.");
    }
}
