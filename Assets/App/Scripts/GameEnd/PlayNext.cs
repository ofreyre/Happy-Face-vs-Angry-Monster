using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNext : MonoBehaviour {

    public GlobalFlow m_flow;
    public Globals m_globals;

    public void OnClick()
    {
        m_flow.level++;
        m_flow.stage = m_flow.level / m_globals.levelsPerStage;
        Debug.Log("PlayNext.OnClick " + m_flow.stage + " " + m_flow.level);
        m_flow.ToScene("ItemsStore");
    }
}
