using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace DBGads
{
    public enum BANNER_SIZE
    {
        Banner,
        MediumRectangle,
        IABBanner,
        Leaderboard,
        SmartBanner
    }

    [Serializable]
    public class AdBanner
    {
        public string name;
        public string id_android;
        public string id_ios;
        public BANNER_SIZE size;
        public AdPosition position;
        AdSize m_size;
        bool m_sizeReady;

        public static AdBanner NONE = new AdBanner("", "", "", BANNER_SIZE.Banner, AdPosition.Bottom);

        public AdBanner(string name, string id_android, string id_ios, BANNER_SIZE size, AdPosition position)
        {
            this.name = name;
            this.id_android = id_android;
            this.id_ios = id_ios;
            this.size = size;
            this.position = position;
            m_size = Size;
        }

        public AdSize Size
        {
            get
            {
                if (!m_sizeReady)
                {
                    switch (size)
                    {
                        case BANNER_SIZE.Banner:
                            m_size = AdSize.Banner;
                            break;
                        case BANNER_SIZE.IABBanner:
                            m_size = AdSize.IABBanner;
                            break;
                        case BANNER_SIZE.Leaderboard:
                            m_size = AdSize.Leaderboard;
                            break;
                        case BANNER_SIZE.MediumRectangle:
                            m_size = AdSize.MediumRectangle;
                            break;
                        //case BANNER_SIZE.SmartBanner:
                        default:
                            m_size = AdSize.SmartBanner;
                            break;
                    }
                    m_sizeReady = true;
                }
                return m_size;
            }
        }
    }

    [Serializable]
    public class AdUnit
    {
        public string name;
        public string id_android;
        public string id_ios;

        public static AdUnit NONE = new AdUnit("", "", "");

        public AdUnit(string name, string id_android, string id_ios)
        {
            this.name = name;
            this.id_android = id_android;
            this.id_ios = id_ios;
        }
    }
}
