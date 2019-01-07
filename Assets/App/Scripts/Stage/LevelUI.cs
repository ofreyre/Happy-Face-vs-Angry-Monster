using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : StageUI
{
    public Image m_frame;
    public Text m_number;

    public Color frameColor
    {
        set { m_frame.color = value; }
    }

    public string number {
        set { m_number.text = value; }
    }
}
