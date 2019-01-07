using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGroup : MonoBehaviour {
    public ToggleButton[] m_options;
    public int m_selectedI;

    void Awake()
    {
        for (int i = 0, n = m_options.Length; i < n; i++)
        {
            m_options[i].m_refreshAtStart = false;
            m_options[i].IsOn = i == m_selectedI;
        }
    }

    public virtual void OnChange(int index)
    {
        SelectedI = index;
    }

    public virtual int SelectedI {
        get { return m_selectedI; }
        set
        {
            if (m_selectedI > -1)
            {
                m_options[m_selectedI].IsOn = false;
            }
            m_selectedI = value;
            m_options[m_selectedI].IsOn = true;
        }
    }
}
