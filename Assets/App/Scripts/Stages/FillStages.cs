using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStages : MonoBehaviour {
    public Globals m_globals;
    public Text m_stars;
    public StageUI[] m_stages;

    // Use this for initialization
    void Start () {
        int levelsPerSTage = m_globals.levelsPerStage;
        int[] stars = DBmanager.GetLevels().stars;
        int starsC = 0, k, stages = m_globals.stages, starsTotal = 0;
        StageUI stageUI;
        for (int i = 0, n = stages; i < n; i++) {
            stageUI = m_stages[i];
            starsC = 0;
            k = i * levelsPerSTage;
            for (int j = 0; j < levelsPerSTage; j++)
            {
                starsC += stars[k + j];
            }
            stageUI.SetStars(starsC, levelsPerSTage * 3);
            starsTotal += starsC;
        }
        m_stars.text = starsTotal.ToString() + "/" + (stages * levelsPerSTage * 3);
    }
}
