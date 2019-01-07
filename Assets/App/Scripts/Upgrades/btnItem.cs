using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnItem : MonoBehaviour
{
    public enum btnItem_State
    {
        disabled,
        enabled,
        used
    }

    public Image m_frame;
    public Image m_icon;
    public Color m_colorDisabled;
    public Color m_colorEnabled;
    public Color m_colorSelected;
    btnItem_State m_state;
    protected bool consumable;

    public btnItem_State State
    {
        set
        {
            m_state = value;
            switch (value)
            {
                case btnItem_State.disabled:
                    Disabled();
                    break;
                case btnItem_State.enabled:
                    Enabled();
                    break;
                case btnItem_State.used:
                    Used();
                    break;

            }
        }

        get {
            return m_state;
        }
    }

    void Disabled()
    {
        m_frame.color = m_colorDisabled;
        m_icon.color = m_colorDisabled;
        consumable = false;
    }

    void Enabled()
    {
        m_frame.color = m_colorEnabled;
        m_icon.color = m_colorEnabled;
        consumable = true;
    }

    void Used()
    {
        m_frame.color = Color.white;
        m_icon.color = Color.white;
        consumable = false;
    }

    public void Select()
    {
        m_frame.color = m_colorSelected;
    }

    public void UnSelect()
    {
        State = m_state;
    }
}
