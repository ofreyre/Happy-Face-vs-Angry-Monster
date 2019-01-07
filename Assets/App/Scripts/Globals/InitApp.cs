using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitApp : MonoBehaviour {
    public GlobalFlow m_flow;

    // Use this for initialization
    void Awake ()
    {
        m_flow.Init();
    }
}
