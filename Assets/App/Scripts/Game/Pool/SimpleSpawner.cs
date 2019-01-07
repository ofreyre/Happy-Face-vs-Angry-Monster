using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject m_prefab;
    public float m_lapse;
    WaitForSeconds local_wait;

    public void Spawn(Vector3 position, int amount)
    {
        if (local_wait == null)
        {
            local_wait = new WaitForSeconds(m_lapse);
        }
        StartCoroutine(_Spawn(position, amount));
    }

    protected IEnumerator _Spawn(Vector3 position, int amount)
    {
        Transform trans;
        while (amount > 0)
        {
            trans = Instantiate(m_prefab).transform;
            trans.position = position;
            amount--;
            yield return local_wait;
        }
    }
}
