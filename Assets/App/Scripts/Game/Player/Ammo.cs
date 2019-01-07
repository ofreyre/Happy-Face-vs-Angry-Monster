using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Text m_UI;
    public GameObject[] m_typeIcons;
    int m_value;
    GameObject m_curentTypeIcon;

    public static Ammo instance;

    void Awake()
    {
        instance = this;
    }

    public int value
    {
        get { return m_value; }
        set
        {
            m_value = value;
            m_UI.text = m_value.ToString();
        }
    }

    public string text {
        get { return m_UI.text; }
        set { m_UI.text = value; }
    }

    public int type {
        set {
            if (m_curentTypeIcon != null) {
                m_curentTypeIcon.SetActive(false);
            }
            m_curentTypeIcon = m_typeIcons[value];
            m_curentTypeIcon.SetActive(true);
        }
    }
}
