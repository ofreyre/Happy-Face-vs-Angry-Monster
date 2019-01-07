using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public LayerMask m_active;
    MonoBehaviour[] m_scripts;

    void OnCollisionEnter2D(Collision2D col)
    {
        /*Geometry geometry = col.gameObject.GetComponent<Geometry>();
        if (geometry != null)
        {
            geometry.gameObject.layer = Mathf.RoundToInt(Mathf.Log(m_active.value, 2));
            geometry.Activate();
            //enabled = false;
        }*/

        Activate(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Activate(collision.gameObject);
    }

    void Activate(GameObject gobj)
    {
        gobj.layer = Mathf.RoundToInt(Mathf.Log(m_active.value, 2));
        m_scripts = gobj.GetComponents<MonoBehaviour>();
        for (int i = 0, n = m_scripts.Length; i < n; i++)
        {
            m_scripts[i].enabled = true;
        }
    }
}
