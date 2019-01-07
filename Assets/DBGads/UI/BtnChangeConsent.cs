using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.InMobi;
using GoogleMobileAds.Api;

namespace DBGads
{
    public class BtnChangeConsent : MonoBehaviour
    {
        public AdManager m_adManager;
        public GlobalFlow m_flow;
        public string m_nextScene;

        public void SetConsent(bool consent)
        {
            m_adManager.SetConsent(consent);
            m_flow.ToScene(m_nextScene);
        }
    }
}
