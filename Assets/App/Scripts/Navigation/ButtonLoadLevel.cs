using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadLevel : ButtonLoadScene
{
    public void Load()
    {
        LoadScene("level"+ m_flow.level.ToString());
    }
}
