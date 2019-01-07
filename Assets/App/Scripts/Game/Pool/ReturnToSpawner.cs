using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSpawner : MonoBehaviour
{
    Geometry m_geometry;
    private void Awake()
    {
        m_geometry = GetComponent<Geometry>();
    }

    void OnDisable()
    {
        Spawner.instance.RemoveEnemy(m_geometry.prefabsIndex, m_geometry.prefabIndex, gameObject);
    }
}
