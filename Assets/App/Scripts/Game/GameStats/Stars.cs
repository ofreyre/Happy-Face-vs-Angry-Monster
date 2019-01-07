using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : SimpleSpawner
{
    public static Stars instance;
    public GameStats m_gameStats;
    int m_value;

    void Awake() {
        instance = this;
        m_gameStats.stars = 0;
    }

	public void Add (int amount, Vector3 position) {
        m_value++;
        Spawn(position, amount);
    }

    public void ToStats()
    {
        m_gameStats.stars = m_value;
    }
}
