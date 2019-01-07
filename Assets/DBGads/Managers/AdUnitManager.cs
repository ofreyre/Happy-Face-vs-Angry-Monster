using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

namespace DBGads
{
    public class AdUnitManager : ScriptableObject
    {
        protected AD_STATE m_state;
        protected AD_TYPE type;
        protected string unitName;
        AdSDB adsDB;
        protected AdRequest request;
        protected string adUnitId;

        public virtual void Init()
        {
        }

        public AdRequest GetRequest()
        {
            adsDB = AdSDB.Get();
            if (adsDB.consent == ADS_CONSENT.relevant)
            {
                return new AdRequest.Builder().Build();
            }
            else
            {
                return new AdRequest.Builder().AddExtra("npa", "1").Build();
            }
        }

        public AD_STATE state
        {
            get { return m_state; }
            set
            {
                m_state = value;
                AdEvents.DispatchAdState(type, m_state, unitName);
            }
        }

        public virtual void HandleOnAdLoaded(object sender, EventArgs args)
        {
            state = AD_STATE.loaded;
        }

        public virtual void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            state = AD_STATE.failToLoad;
            //Request();
        }

        public virtual void HandleOnAdOpened(object sender, EventArgs args)
        {
            state = AD_STATE.opened;
        }

        public virtual void HandleOnAdClosed(object sender, EventArgs args)
        {            
            state = AD_STATE.idle;
        }

        public virtual void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
        }

        protected virtual void Request()
        {
        }

        protected virtual void RequestRetry()
        {
        }

        public virtual void Show()
        {
        }

        public virtual void Clear()
        {
            m_state = AD_STATE.idle;
        }

        public virtual void Hide()
        {
            Clear();
            Request();
        }
    }
}
