using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillCoins : MonoBehaviour
{
    public GameEndEventManager m_events;
    public GameStats m_gameStats;
    public GlobalFlow m_flow;
    public GameObject m_coin;
    public float m_updateTextDelay = 0.3f;
    [SerializeField]
    Text m_text;

    // Use this for initialization
    void Start ()
    {
        if (m_flow.PrevScene.Contains("level"))
        {
            DBmanager.Coins += m_gameStats.score / m_gameStats.pointsPerCoin;
            m_events.AddCoin += Animate;
            m_events.ScoreEnd += OnScoreEnd;
        }
        else
        {
            Fill();
        }
    }

    void Animate(int score) {
        if (!m_coin.activeSelf)
        {
            m_coin.SetActive(true);
        }
        StartCoroutine(_Animate(score / m_gameStats.pointsPerCoin));
    }

    void OnScoreEnd()
    {
        m_coin.SetActive(false);
    }

    IEnumerator _Animate(int coins) {
        yield return new WaitForSeconds(m_updateTextDelay);
        m_text.text = coins.ToString();
    }

    void Fill() {
        m_text.text = (m_gameStats.score / m_gameStats.pointsPerCoin).ToString();
    }
}
