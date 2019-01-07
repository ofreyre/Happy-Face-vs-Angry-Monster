using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBGads
{
    public class WatchVideo : MonoBehaviour
    {

        [SerializeField]
        AdManager m_adManager;
        [SerializeField]
        BannersDisplay m_bannerDisplay;
        [SerializeField]
        float m_waitForResponse = 5;
        [SerializeField]
        Globals m_globals;
        [SerializeField]
        GameObject m_btnWatch;
        bool m_unlocked;
        bool m_listenFail;
        bool m_videoClosed;
        bool m_messageDisplayed;
        float m_t;

        public static WatchVideo instance;
        public delegate void DelegateReward(int amount);
        public event DelegateReward Reward;

        public void DispatchReward(int amount)
        {
            if (Reward != null)
            {
                Reward(amount);
            }
        }

        public void Awake()
        {
            instance = this;
        }

        public void OnEnable()
        {
            m_unlocked = false;
            //bool videoReady = m_adManager.IsVideoReady();
            m_btnWatch.SetActive(true);
            m_btnWatch.GetComponent<Button>().interactable = true;
            m_adManager.ClearBanners();
        }

        void OnDestroy()
        {
            m_listenFail = false;
            AdEvents.AdState -= Result;
            m_listenFail = false;
        }

        public void OnWatchVideo()
        {
            m_bannerDisplay.Hide();
            m_videoClosed = false;
            m_messageDisplayed = false;
            m_btnWatch.GetComponent<Button>().interactable = false;
            m_listenFail = true;
            WaitForResponseStart();
            AdEvents.AdState -= Result;
            AdEvents.AdState += Result;
            m_adManager.ShowVideo();
        }

        void Result(AD_TYPE adType, AD_STATE state, string unitName)
        {
            if (adType == AD_TYPE.video)
            {
                switch (state)
                {
                    case AD_STATE.loading:
                        break;
                    case AD_STATE.failToLoad:
                        ResponseFail();
                        break;
                    case AD_STATE.loaded:
                        m_listenFail = false;
                        break;
                    case AD_STATE.started:
                        m_listenFail = false;
                        break;
                    case AD_STATE.rewarded:
                        m_listenFail = false;
                        m_unlocked = true;
                        DisplayMessage();
                        break;
                    case AD_STATE.idle:
                        VideoClosed();
                        break;
                }
            }
        }

        void WaitForResponseStart()
        {
            m_t = Time.time + m_waitForResponse;
            m_listenFail = true;
        }

        void Update()
        {
            if (m_listenFail)
            {
                if (Time.time > m_t)
                {
                    ResponseFail();
                }
            }
        }

        void ResponseFail()
        {
            m_listenFail = false;
            //m_adManager.managerVideo.Hide();
            AdEvents.AdState -= Result;
            m_unlocked = true;
            m_btnWatch.SetActive(true);
            m_btnWatch.GetComponent<Button>().interactable = true;

            if (m_bannerDisplay != null)
            {
                m_bannerDisplay.gameObject.SetActive(true);
            }

            EventManagerMessages.instance.DispatchFailure("Sorry, no video available.\nPlease try later.");
        }

        void DisplayMessage()
        {
            if (m_videoClosed && !m_messageDisplayed)
            {
                AdEvents.AdState -= Result;
                m_messageDisplayed = true;
                DispatchReward(m_globals.coinsPerVideo);
                EventManagerMessages.instance.DispatchSuccess("Congratulations, you earned 100 coins!");
            }
        }

        public void VideoClosed()
        {
            m_videoClosed = true;
            m_listenFail = false;
            if (m_unlocked)
            {
                DisplayMessage();
            }
            m_btnWatch.SetActive(true);
            m_btnWatch.GetComponent<Button>().interactable = true;
        }
    }
}
