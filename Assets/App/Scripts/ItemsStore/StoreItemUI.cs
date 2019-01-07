using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemUI : ItemUI
{
    public Image m_frame;
    public Color m_colorDisabled;
    public Color m_colorSelected;
    public Text m_priceUI;
    public bool m_consumable;

    public override void Fill(DBinventoryItem item, int amount)
    {
        m_priceUI.text = item.price.ToString();
        base.Fill(item, amount);
    }

    //value == money
    public override bool Enable(int value = 0)
    {
        m_consumable = value >= m_item.price;
        if (m_consumable)
        {
            m_frame.color = Color.white;
            m_image.color = Color.white;
        }
        else
        {
            m_frame.color = m_colorDisabled;
            m_image.color = m_colorDisabled;
        }
        return m_consumable;
    }

    public override void OnClick()
    {
        Select();
        base.OnClick();
    }

    public void Select()
    {
        m_frame.color = m_colorSelected;
    }

    public void Unselect()
    {
        if (m_consumable)
        {
            m_frame.color = Color.white;
        }
        else
        {
            m_frame.color = m_colorDisabled;
        }
    }
}
