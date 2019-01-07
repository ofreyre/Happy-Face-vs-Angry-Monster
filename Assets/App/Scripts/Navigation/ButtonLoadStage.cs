using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadStage : ButtonLoadScene
{
    public void Load(int stageN)
    {
        m_flow.stage = stageN;
        LoadScene("Stage");
    }
}
