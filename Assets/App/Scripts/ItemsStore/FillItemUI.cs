using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillItemUI : MonoBehaviour
{
    public Text m_name;
    public Text m_coins;
    public Text m_description;

    // Use this for initialization
    void Start ()
    {
        EventManager.instance.ItemSelected += Selected;
    }
	
	void Selected(ItemUI itemUI)
    {
        m_name.text = itemUI.m_item.name;
        m_coins.text = itemUI.m_item.price.ToString();
        m_coins.gameObject.SetActive(true);
        m_description.text = itemUI.m_item.description;

    }
}
