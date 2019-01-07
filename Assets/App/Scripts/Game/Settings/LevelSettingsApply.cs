using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettingsApply : MonoBehaviour {
    public LevelsSettings m_settings;
    public GlobalFlow m_globalFlow;

    void Awake() {

        LevelSettings settings = m_settings.levels[m_globalFlow.level];

        Scroller scroller = GameObject.Find("Controller").GetComponent<Scroller>();
        scroller.m_speedMin = settings.scroller.speedMin;
        scroller.m_speedMax = settings.scroller.speedMax;
        scroller.m_accelerationDuration = settings.scroller.accelerationDuration;
        scroller.m_accelerationLapse = settings.scroller.accelerationLapse;

        WallSettings(GameObject.Find("wallLeft").GetComponent<Wall>(), settings.wallSides);
        WallSettings(GameObject.Find("wallRight").GetComponent<Wall>(), settings.wallSides);
        WallSettings(GameObject.Find("wallBottom").GetComponent<Wall>(), settings.wallBottom);
        WallSettings(GameObject.Find("wallTop").GetComponent<Wall>(), settings.wallTop);
    }

    void WallSettings(Wall wall, LevelSettingsWall settings)
    {
        wall.m_bounceK = settings.bounceK;
        wall.m_bounceMin = settings.bounceMin;
    }

    void Start()
    {
        Destroy(gameObject);
    }
}
