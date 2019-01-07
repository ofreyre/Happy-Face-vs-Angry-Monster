using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace DBGads
{
    public class AdBannerManager : AdUnitManager
    {
        public AdBanner unit;
        BannerView banner;
        bool hide = false;
        bool displayed = false;

        public override void Init()
        {
#if UNITY_ANDROID
            adUnitId = unit.id_android;
#elif UNITY_IPHONE
            adUnitId = unit.id_ios;
#else
            adUnitId = "unexpected_platform";
#endif
            type = AD_TYPE.banner;
            unitName = unit.name;
            base.Init();
        }

        public override void HandleOnAdLoaded(object sender, EventArgs args)
        {
            base.HandleOnAdLoaded(sender, args);
            if (hide)
            {
                banner.Hide();
            }
            else
            {
                displayed = true;
            }
        }

        protected override void Request()
        {
            if (m_state != AD_STATE.loading)
            {
                if (banner != null)
                {
                    banner.Destroy();
                }
                banner = new BannerView(adUnitId, unit.Size, unit.position);
                // Called when an ad request has successfully loaded.
                banner.OnAdLoaded += HandleOnAdLoaded;
                // Called when an ad request failed to load.
                banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                // Called when an ad is clicked.
                banner.OnAdOpening += HandleOnAdOpened;
                // Called when the user returned from the app after an ad click.
                banner.OnAdClosed += HandleOnAdClosed;
                // Called when the ad click caused the user to leave the application.
                banner.OnAdLeavingApplication += HandleOnAdLeavingApplication;
                RequestRetry();
            }
        }

        protected override void RequestRetry()
        {
            request = GetRequest();
            state = AD_STATE.loading;
            banner.LoadAd(request);
        }

        public override void Show()
        {
            switch (state)
            {
                case AD_STATE.loaded:
                    if (banner == null)
                    {
                        base.Show();
                        Request();
                    }
                    else if (displayed)
                    {
                        hide = false;
                        Request();
                    }
                    else
                    {
                        banner.Show();
                    }
                    break;
                case AD_STATE.failToLoad:
                    hide = false;
                    break;
                case AD_STATE.idle:
                    hide = false;
                    Request();
                    break;
                case AD_STATE.loading:
                    hide = false;
                    break;

            }
        }

        public override void Hide()
        {
            Clear();
        }

        public override void Clear()
        {
            hide = true;
            if (IsReady)
            {
                banner.Hide();
            }
        }

        public bool IsReady
        {
            get { return m_state == AD_STATE.loaded && banner != null; }
        }
    }
}
