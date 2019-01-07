using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBank : ScriptableObject {
    public Spawner.Prefabs[] m_prefabs;
    public Spawner.PrefabsCount[] m_prefabsCounts;
}
