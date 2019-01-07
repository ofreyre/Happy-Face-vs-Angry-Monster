using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveRateCoins : MonoBehaviour
{
    public GameObject m_target;

    public void OnClick()
    {
        int coinsForRate = DBmanager.CoinsForRate.amount;
        if (coinsForRate > 0)
        {
            DBmanager.Coins += coinsForRate;
            DBmanager.SetCoinsForRate(0);
            m_target.SetActive(false);
        }
    }
}
