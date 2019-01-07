using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSettingsSpawnerApply : MonoBehaviour {

    public LevelsSettingsSpawner m_settings;
    public SpawnerBank m_bank;
    public GlobalFlow m_flow;

    // Use this for initialization
    void Awake () {
        Spawner spawner = GameObject.Find("Controller").GetComponent<Spawner>();
        int[] settings = m_settings.spawners[m_flow.level].array;
        int n = 0;
        for (int i = 0, m = settings.Length; i < m; i++) {
            n = Mathf.Max(settings[i]);
        }
        spawner.m_prefabs = new Spawner.Prefabs[n+1];
        spawner.m_prefabsCounts = new Spawner.PrefabsCount[n+1];

        Spawner.Prefabs[] prefabs = m_bank.m_prefabs;
        Spawner.PrefabsCount[] prefabsCounts = m_bank.m_prefabsCounts;

        for (int i = 0, m = settings.Length; i < m; i++)
        {
            n = settings[i];
            spawner.m_prefabs[n] = prefabs[n];
            spawner.m_prefabsCounts[n] = prefabsCounts[n];
        }
    }

    void Start() {
    }
}
