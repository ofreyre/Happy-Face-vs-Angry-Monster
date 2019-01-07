using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMenu : MonoBehaviour
{
    public GameStats m_gameStats;
    public GlobalFlow m_flow;
    public GameEndEventManager m_events;
    public GameObject m_gameObject;
    public GameObject m_tips;

    // Use this for initialization
    void Awake()
    {
        if (m_flow.PrevScene.Contains("level"))
        {
            m_gameObject.SetActive(false);
            m_events.StarsEnd += OnStarsEnd;
        }
        else
        {
            m_gameObject.SetActive(true);
            OnStarsEnd(m_gameStats.stars);
        }
    }

    void OnStarsEnd(int stars)
    {
        int freeStars = DBmanager.Stars - DBmanager.Upgrades.TotalStars;
        m_tips.SetActive(freeStars > 0);
        m_gameObject.SetActive(true);
    }
}
