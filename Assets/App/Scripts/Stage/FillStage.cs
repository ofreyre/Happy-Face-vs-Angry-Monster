using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStage : MonoBehaviour {

    public Globals m_globals;
    public Text m_stars;
    public FillLevels m_fillLevels;

	// Use this for initialization
	void Start () {
        m_stars.text = m_fillLevels.Fill() + "/" + (m_globals.levelsPerStage * 3);
    }
}
