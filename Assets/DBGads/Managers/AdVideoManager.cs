using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace DBGads
{
    public class AdVideoManager : AdUnitManager
    {
        public AdUnit unit;
        private RewardBasedVideoAd rewardBasedVideo;
        bool display = false;

        public override void Init()
        {
#if UNITY_ANDROID
            adUnitId = unit.id_android;
#elif UNITY_IPHONE
            adUnitId = unit.id_ios;
#else
            adUnitId = "unexpected_platform";
#endif      
            type = AD_TYPE.video;
            unitName = unit.name;
            base.Init();

            rewardBasedVideo = RewardBasedVideoAd.Instance;

            // Called when the ad starts to play.
            rewardBasedVideo.OnAdStarted += HandleVideoStarted;
            // Called when the user should be rewarded for watching a video.
            rewardBasedVideo.OnAdRewarded += HandleVideoRewarded;
            // Called when the ad is closed.

            rewardBasedVideo.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            rewardBasedVideo.OnAdOpening += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            rewardBasedVideo.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            Request();
        }

        public override void HandleOnAdLoaded(object sender, EventArgs args)
        {
            base.HandleOnAdLoaded(sender, args);
            if (display)
            {
                rewardBasedVideo.Show();
            }
        }

        public override void HandleOnAdClosed(object sender, EventArgs args)
        {
            base.HandleOnAdClosed(sender, args);
            display = false;
            Request();
        }


        public void HandleVideoStarted(object sender, EventArgs args)
        {
            state = AD_STATE.started;
        }

        public void HandleVideoRewarded(object sender, EventArgs args)
        {
            state = AD_STATE.rewarded;
        }

        protected override void Request()
        {
            RequestRetry();
        }

        protected override void RequestRetry()
        {
            state = AD_STATE.loading;
            request = GetRequest();
            rewardBasedVideo.LoadAd(request, adUnitId);
        }

        public override void Show()
        {
            switch (state)
            {
                case AD_STATE.loaded:
                    if (rewardBasedVideo != null && rewardBasedVideo.IsLoaded())
                    {
                        rewardBasedVideo.Show();
                        base.Show();
                    }
                    else
                    {
                        display = true;
                        Request();
                    }
                    break;
                case AD_STATE.loading:
                    display = true;
                    break;
                case AD_STATE.failToLoad:
                case AD_STATE.rewarded:
                case AD_STATE.idle:
                    display = true;
                    Request();
                    break;
            }
        }

        public override void Hide()
        {
            display = false;
            base.Hide();
        }

        public override void Clear()
        {
            display = false;
            base.Clear();
        }

        public bool IsReady
        {
            get { return rewardBasedVideo != null && rewardBasedVideo.IsLoaded(); }
        }
    }
}
