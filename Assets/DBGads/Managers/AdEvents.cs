using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGads
{
    public enum AD_STATE
    {
        idle,
        loading,
        loaded,
        opened,
        failToLoad,
        started,
        rewarded
    }

    public enum AD_TYPE
    {
        banner,
        interstitial,
        video
    }

    public class AdEvents
    {
        public delegate void DelegateAdState(AD_TYPE adType, AD_STATE state, string unitName);
        public static event DelegateAdState AdState;

        public static void DispatchAdState(AD_TYPE adType, AD_STATE state, string unitName)
        {
            if (AdState != null)
            {
                AdState(adType, state, unitName);
            }
        }

        public static void Clear()
        {
            AdState = null;
        }
    }
}
