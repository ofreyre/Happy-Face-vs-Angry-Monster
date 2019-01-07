using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;

public enum MESSAGE_TYPE
{
    start,
    enable,
    disable,
    destroy
}

public class InterstitialDisplay : MonoBehaviour
{
    public AdManager m_adManager;
    public string m_interstitial;
    public MESSAGE_TYPE m_messageThatDisplays;

    // Use this for initialization
    void Start ()
    {
        if (m_messageThatDisplays == MESSAGE_TYPE.start)
        {
            m_adManager.ShowInterstitial(m_interstitial);
        }
    }

    void OnEnable()
    {
        if (m_messageThatDisplays == MESSAGE_TYPE.enable)
        {
            m_adManager.ShowInterstitial(m_interstitial);
        }
    }

    void OnDisable()
    {
        if (m_messageThatDisplays == MESSAGE_TYPE.disable)
        {
            m_adManager.ShowInterstitial(m_interstitial);
        }
    }

    void OnDestroy()
    {
        if (m_messageThatDisplays == MESSAGE_TYPE.destroy)
        {
            m_adManager.ShowInterstitial(m_interstitial);
        }
    }
}
