using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillStars : MonoBehaviour
{
    public GameEndEventManager m_events;
    public GlobalFlow m_flow;
    public GameStats m_gameStats;
    public GameObject[] m_stars;
    public float m_lapse = 0.5f;

	// Use this for initialization
	void Awake ()
    {
        if (m_flow.PrevScene.Contains("level"))
        {
            m_events.ScoreEnd += Animate;
        }
        else {
            Fill();
        }
    }

    void Animate()
    {
        StartCoroutine(_Animate());
    }
    
    IEnumerator _Animate()
    {
        int gameStars = m_gameStats.stars;
        int stars = 0;
        while (stars < gameStars)
        {
            m_stars[stars].SetActive(true);
            stars++;
            yield return new WaitForSeconds(m_lapse);
        }
        m_events.DispatchStarsEnd(stars);
    }

    void Fill() {
        for (int i = 0, n = m_gameStats.stars; i < n; i++) {
            m_stars[i].SetActive(true);
        }
    }
}
