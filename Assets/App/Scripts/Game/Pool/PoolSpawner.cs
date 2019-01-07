using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour {

    public Pool m_pool;
    public float m_lapse;
    GameObject local_gobj;
    WaitForSeconds local_wait;

    public void Spawn(Vector3 position, int amount)
    {
        if (local_wait == null) {
            local_wait = new WaitForSeconds(m_lapse);
        }
        StartCoroutine(_Spawn(position, amount));
    }

    protected IEnumerator _Spawn(Vector3 position, int amount)
    {
        while (amount > 0)
        {
            local_gobj = m_pool.Get();
            if (local_gobj != null)
            {
                local_gobj.SetActive(true);
                local_gobj.transform.position = position;
                amount--;
                yield return local_wait;
            }
            else {
                amount = 0;
            }
        }
    }
}
