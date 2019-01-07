using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FixApplySettings : EditorWindow
{

    public static void Display()
    {
        FixApplySettings instance = GetWindow<FixApplySettings>();
        instance.titleContent = new GUIContent("Fix ApplySettings");
    }

    private void OnGUI()
    {
        /*if (GUILayout.Button("Fix"))
        { 
            LevelSettingsApply LevelSettings = GameObject.Find("ApplySetings").GetComponent<LevelSettingsApply>();


            Transform controller = GameObject.Find("Controller").transform;
            LevelSettings.m_scroller = controller.GetComponent<Scroller>();
            LevelSettings.m_wallLeft = controller.Find("wallLeft").GetComponent<Wall>();
            LevelSettings.m_wallRight = controller.Find("wallRight").GetComponent<Wall>();
            LevelSettings.m_wallBottom = controller.Find("wallBottom").GetComponent<Wall>();
            LevelSettings.m_wallTop = controller.Find("wallTop").GetComponent<Wall>();
        }*/
    }
}
