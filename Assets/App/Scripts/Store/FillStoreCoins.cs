using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DBGads;

public class FillStoreCoins : MonoBehaviour
{
    public EventManagerMessages m_eventManager;
    public Text text;

    // Use this for initialization
    void Start () {
        m_eventManager.Int += Fill;
        WatchVideo.instance.Reward += AddCoins;
    }
	
	// Update is called once per frame
	void Fill (int coins) {
        text.text = DBmanager.Coins.ToString();
	}

    void AddCoins(int coins)
    {
        DBmanager.Coins += coins;
        Fill(coins);
    }
}
