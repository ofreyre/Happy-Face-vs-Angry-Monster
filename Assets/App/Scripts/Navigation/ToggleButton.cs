using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {

    public GameObject m_on;
    public GameObject m_off;
    public bool m_isOn;
    public bool m_refreshAtStart = true;

    // Use this for initialization
    protected virtual void Start() {
        if (m_refreshAtStart)
        {
            Refresh();
        }
    }

    // Update is called once per frame
    public virtual void Toggle()
    {
        m_isOn = !m_isOn;
        Refresh();
    }

    public virtual bool IsOn{
        get {
            return m_isOn;
        }

        set {
            m_isOn = value;
            Refresh();
        }
    }

    void Refresh()
    {
        m_on.SetActive(m_isOn);
        m_off.SetActive(!m_isOn);
    }
}
