using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildAtCenter : MonoBehaviour {
    public GameObject m_child;
    GameObject local_gobj;

    // Use this for initialization
    void Start () {
        local_gobj = Instantiate(m_child);
        local_gobj.transform.SetParent(transform);
        local_gobj.transform.localPosition = Vector3.zero;
	}
}
