using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : ToggleButton
{
	// Use this for initialization
	protected override void Start () {
        m_isOn = DBmanager.AudioOn;
        base.Start();
    }

    public override void Toggle()
    {
        base.Toggle();
        DBmanager.AudioOn = m_isOn;
        Debug.Log(m_isOn);
    }
}
