using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControls : ToggleGroup
{
    public string[] m_descriptions;
    public Text m_description;
	// Use this for initialization
	void Start () {
        SelectedI = (int)DBmanager.UserControls;
    }

    public override int SelectedI
    {
        set
        {
            base.SelectedI = value;
            DBmanager.UserControls = (USERVECTOR_TYPE)SelectedI;
            m_description.text = m_descriptions[value];
        }
    }
}
