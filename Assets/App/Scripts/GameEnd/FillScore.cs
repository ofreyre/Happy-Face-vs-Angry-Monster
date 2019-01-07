using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillScore : MonoBehaviour
{
    public GameEndEventManager m_events;
    public GameStats m_gameStats;
    public GlobalFlow m_flow;
    public Globals m_globals;
    [SerializeField]
    float m_duration = 3;
    [SerializeField]
    float m_incLapse = 0.1f;
    [SerializeField]
    Text m_text;
    [SerializeField]
    GameObject m_playNext;

    // Use this for initialization
    void Start ()
    {
        if (m_flow.PrevScene.Contains("level"))
        {
            Debug.Log("SaveLevel "+ m_flow.level+"  score = "+ m_gameStats.score+"  stars ="+ m_gameStats.stars);
            DBmanager.SaveLevel(m_flow.level, m_gameStats.score, m_gameStats.stars);

            StartCoroutine(Animate());
        }
        else
        {
            Fill();
        }

        if (DBmanager.GetUnlockLevelLast(m_flow.stage) - 1 >= m_flow.level && m_flow.level < m_globals.LevelsCount - 1)
        {
            m_playNext.SetActive(true);
        }
        else
        {
            m_playNext.SetActive(false);
        }
    }

    IEnumerator Animate()
    {
        int gameScore = m_gameStats.score;
        float scoreInc = gameScore * m_incLapse / m_duration;
        m_text.text = "0";
        float score = 0;
        float pointsPerCoin = m_gameStats.pointsPerCoin;
        float lastCoinScore = pointsPerCoin;
        while (score < gameScore)
        {
            yield return new WaitForSeconds(m_incLapse);
            score += scoreInc;
            m_text.text = ((int)score).ToString();
            if (score >= lastCoinScore) {
                lastCoinScore += pointsPerCoin;
                m_events.DispatchAddCoin((int)score);
            }
        }
        m_text.text = gameScore.ToString();
        //AudioManager.instance.Stop("scoreup");
        
        m_events.DispatchScoreEnd();
    }

    void Fill() {
        m_text.text = m_gameStats.score.ToString();
    }
}
