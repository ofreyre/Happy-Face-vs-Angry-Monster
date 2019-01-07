using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;

public class BannersDisplay : MonoBehaviour {
    public AdManager m_adManager;
    public string[] m_banners;
    int m_bannerCount;

    private void OnEnable()
    {
        Show();
    }

    private void OnDisable()
    {
        Hide();
    }

    private void OnDestroy()
    {
        Hide();
    }

    public void Show()
    {
        for (int i = 0; i < m_banners.Length; i++)
        {
            m_adManager.ShowBanner(m_banners[i]);
        }
    }

    public void Hide()
    {
        m_adManager.ClearBanners();
    }
}
