using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api.Mediation.AppLovin;

namespace DBGads
{
    public class AdInit : MonoBehaviour
    {
        public AdManager m_adManager;
        public string m_nextScene;
        public string m_GDPRscene;

        // Use this for initialization
        void Start()
        {
            Init();
        }

        void Init()
        {
            m_adManager.initialized = false;

            if (AdSDB.Get() != null)
            {
                m_adManager.Init();
                GetComponent<Splash>().m_nextScene = m_nextScene;
            }
            else
            {
                GetComponent<Splash>().m_nextScene = m_GDPRscene;
            }
        }
    }
}
