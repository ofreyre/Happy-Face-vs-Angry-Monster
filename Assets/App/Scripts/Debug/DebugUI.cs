using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {
    Text m_logUI;
    public static DebugUI instance;

    // Use this for initialization
    void Awake () {
        instance = this;
        this.m_logUI = GetComponent<Text>();
    }

    // Update is called once per frame
    public static void Log (string log) {
        if (instance != null)
        {
            instance.m_logUI.text += log + "\n";
        }
    }
}
