using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct BMnumber {
    public int value;
    public Pool pool;
}

public class Score : MonoBehaviour
{
    public Text m_UI;
    //public BMnumber[] m_numberPools;
    public Transform m_pointsContainer;
    public GameStats m_gameStats;
    public PoolSpawner m_coinSpawner;
    int m_value;
    int m_pointsPerCoin;
    int m_lastCoinScore;

    public static Score instance;

    void Awake() {
        instance = this;
        m_pointsPerCoin = m_gameStats.pointsPerCoin;
        m_lastCoinScore = 0;
        m_gameStats.score = 0;
        value = 0;
    }

    // Use this for initialization
    void Start () {
		
	}

    public int value {
        get { return m_value; }
        set {
            m_value = value;
            m_UI.text = m_value.ToString();
        }
    }

    public void Add(int val, Vector3 position)
    {
        value += val;        
        if (m_value >= m_lastCoinScore + m_pointsPerCoin)
        {
            m_coinSpawner.Spawn(position, ((m_value - m_lastCoinScore) / m_pointsPerCoin));
            m_lastCoinScore = (m_value / m_pointsPerCoin) * m_pointsPerCoin;
        }
    }

    public void ToStats() {
        m_gameStats.score = m_value;
    }
}
