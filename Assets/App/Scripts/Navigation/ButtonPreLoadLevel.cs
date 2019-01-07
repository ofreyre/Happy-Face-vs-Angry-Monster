using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPreLoadLevel : ButtonLoadScene
{
    public string m_prevScene;
    public void Load(int level)
    {
        m_flow.level = level;
        LoadScene(m_prevScene);
    }
}
