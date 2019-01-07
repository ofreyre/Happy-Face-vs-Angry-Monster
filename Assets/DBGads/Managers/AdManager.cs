using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.AppLovin;
using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.InMobi;
using GoogleMobileAds.Api.Mediation.MyTarget;

namespace DBGads
{
    public class AdManager : ScriptableObject
    {
        public string admob_android_appID;
        public string admob_ios_appID;
        public AdBannerManager[] managerBanners;
        public AdInterstitialManager[] managerInterstitials;
        public AdVideoManager managerVideo;
        AdInterstitialManager managerInterstitial;
        public bool initialized;


        public void Init()
        {
            if (!initialized)
            {
#if UNITY_ANDROID
                string appId = admob_android_appID;
#elif UNITY_IPHONE
            string appId = admob_ios_appID;
#else
            string appId = "unexpected_platform";
#endif

                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(appId);

                AppLovin.Initialize();

                InitConsent();

                for (int i = 0, n = managerBanners.Length; i < n; i++)
                {
                    managerBanners[i].Init();
                }

                for (int i = 0, n = managerInterstitials.Length; i < n; i++)
                {
                    managerInterstitials[i].Init();
                }

                if (managerVideo != null)
                {
                    managerVideo.Init();
                }
                initialized = true;
            }
        }



        public void SetConsent(bool consent)
        {
            Consent = consent ? ADS_CONSENT.relevant : ADS_CONSENT.random;
        }

        public ADS_CONSENT Consent
        {
            get
            {
                AdSDB db = AdSDB.Get();
                if (db != null)
                {
                    return ADS_CONSENT.none;
                }
                return db.consent;
            }

            set
            {
                AdSDB.Save(value);
                Init();
            }
        }

        public void InitConsent()
        {
            ADS_CONSENT consent = AdSDB.Get().consent;

            /*UnityAds
            * Integrating Unity Ads with Mediation. Scroll to end:
            * https://developers.google.com/admob/unity/mediation/unity
            * 
            */
            UnityAds.SetGDPRConsentMetaData(consent == ADS_CONSENT.relevant ? true : false);


            /*InMobi
             * Integrating InMobi with Mediation. Scroll to end:
             * https://developers.google.com/admob/unity/mediation/inmobi
             * 
             * More information about the possible keys and values that InMobi accepts in this consent object
             * https://support.inmobi.com/monetize/android-guidelines#h3-null-initializing-the-sdk
             * 
             */
            Dictionary<string, string> consentObject = new Dictionary<string, string>();
            consentObject.Add("gdpr_consent_available", consent == ADS_CONSENT.relevant ? "true" : "false");
            consentObject.Add("gdpr", "1");
            InMobi.UpdateGDPRConsent(consentObject);



            AppLovin.SetHasUserConsent(consent == ADS_CONSENT.relevant ? true : false);


            MyTarget.SetUserConsent(consent == ADS_CONSENT.relevant ? true : false);
        }

        AdBannerManager GetBanner(string name)
        {
            AdBannerManager managerBanner;
            for (int i = 0, n = managerBanners.Length; i < n; i++)
            {
                managerBanner = managerBanners[i];
                if (managerBanner.unit.name == name)
                {
                    return managerBanner;
                }
            }
            return null;
        }

        public void ShowBanner(string name)
        {
            AdBannerManager managerBanner = GetBanner(name);
            if (managerBanner != null)
            {
                managerBanner.Show();
            }
        }

        public void ShowBanners()
        {
            for (int i = 0, n = managerBanners.Length; i < n; i++)
            {
                managerBanners[i].Show();
            }
        }

        public void HideBanner(string name)
        {
            AdBannerManager managerBanner = GetBanner(name);
            if (managerBanner != null)
            {
                managerBanner.Hide();
            }
        }

        public void HideBanners()
        {
            for (int i = 0, n = managerBanners.Length; i < n; i++)
            {
                managerBanners[i].Hide();
            }
        }

        public bool IsBannerReady(string name)
        {
            AdBannerManager managerBanner = GetBanner(name);
            if (managerBanner == null)
            {
                return false;
            }
            return managerBanner.IsReady;
        }

        public void ClearBanner(string name)
        {
            AdBannerManager managerBanner = GetBanner(name);
            if (managerBanner != null)
            {
                managerBanner.Clear();
            }
        }

        public void ClearBanners()
        {
            for (int i = 0, n = managerBanners.Length; i < n; i++)
            {
                managerBanners[i].Clear();
            }
        }

        AdInterstitialManager GetInterstitial(string name)
        {

            for (int i = 0, n = managerInterstitials.Length; i < n; i++)
            {
                managerInterstitial = managerInterstitials[i];
                if (managerInterstitial.unit.name == name)
                {
                    return managerInterstitial;
                }
            }
            return null;
        }

        public void ShowInterstitial(string name)
        {
            managerInterstitial = GetInterstitial(name);
            if (managerInterstitial != null)
            {
                managerInterstitial.Show();
            }
        }

        public bool IsInterstitialReady(string name)
        {
            managerInterstitial = GetInterstitial(name);
            if (managerInterstitial == null)
            {
                return false;
            }
            return managerInterstitial.IsReady;
        }

        public void HideInterstitial(string name)
        {
            managerInterstitial = GetInterstitial(name);
            if (managerInterstitial != null)
            {
                managerInterstitial.Hide();
            }
        }

        public void ClearInterstitial()
        {
            for (int i = 0, n = managerInterstitials.Length; i < n; i++)
            {
                managerInterstitials[i].Clear();
            }
        }

        public void ShowVideo()
        {
            managerVideo.Show();
        }

        public bool IsVideoReady()
        {
            return managerVideo.IsReady;
        }

        public void Clear()
        {
            for (int i = 0, n = managerBanners.Length; i < n; i++)
            {
                managerBanners[i].Clear();
            }

            for (int i = 0, n = managerInterstitials.Length; i < n; i++)
            {
                managerInterstitials[i].Clear();
            }
            managerVideo.Clear();
            AdEvents.Clear();
        }
    }
}
