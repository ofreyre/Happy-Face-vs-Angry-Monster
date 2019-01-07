using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace DBGads
{
    public class AdInterstitialManager : AdUnitManager
    {
        public AdUnit unit;
        InterstitialAd interstitial;

        public override void Init()
        {
#if UNITY_ANDROID
            adUnitId = unit.id_android;
#elif UNITY_IPHONE
            adUnitId = unit.id_ios;
#else
            adUnitId = "unexpected_platform";
#endif
            type = AD_TYPE.interstitial;
            unitName = unit.name;
            base.Init();
            Request();
        }

        public override void HandleOnAdClosed(object sender, EventArgs args)
        {
            base.HandleOnAdClosed(sender, args);
            Request();
        }

        protected override void Request()
        {
            if (interstitial != null)
            {
                interstitial.Destroy();
            }
            interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitial.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            interstitial.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            RequestRetry();
        }

        protected override void RequestRetry()
        {
            //Create an empty ad request.
            request = GetRequest();

            // Load the banner with the request.
            state = AD_STATE.loading;
            interstitial.LoadAd(request);
        }

        public override void Show()
        {
            switch (state)
            {
                case AD_STATE.loaded:
                    if (interstitial != null)
                    {
                        interstitial.Show();
                        base.Show();
                    }
                    else
                    {
                        Request();
                    }
                    break;
                case AD_STATE.loading:
                    break;
                case AD_STATE.failToLoad:
                case AD_STATE.rewarded:
                case AD_STATE.idle:
                    Request();
                    break;
            }
        }

        public override void Clear()
        {
            if (interstitial != null)
            {
                interstitial.Destroy();
            }
            base.Clear();
        }

        public bool IsReady
        {
            get { return m_state == AD_STATE.loaded && interstitial != null; }
        }
    }
}
