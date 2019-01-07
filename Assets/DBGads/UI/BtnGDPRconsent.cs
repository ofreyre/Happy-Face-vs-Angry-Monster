using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.InMobi;
using GoogleMobileAds.Api.Mediation.AppLovin;
using GoogleMobileAds.Api;

namespace DBGads
{
    public class BtnGDPRconsent : MonoBehaviour
    {
        public AdManager m_adManager;
        public string m_nextScene;

        public void SetConsent(bool consent)
        {
            m_adManager.SetConsent(consent);
            SceneManager.LoadScene(m_nextScene);
        }
    }
}
