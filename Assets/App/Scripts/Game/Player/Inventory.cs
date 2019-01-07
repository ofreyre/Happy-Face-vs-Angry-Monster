using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    List<GameObject> m_bombs = new List<GameObject>();

    void Start() {
    }

    void OnBombCollected(GameObject collectable)
    {
        m_bombs.Add(collectable);
    }
}
