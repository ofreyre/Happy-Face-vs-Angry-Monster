using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public class OnClickActiveIfCoinsForRate : MonoBehaviour
{
    public GameObject m_target;

    public void OnClick()
    {
        if (DBmanager.CoinsForRate.amount > 0)
        {
            m_target.SetActive(true);
        }
        else
        {
            //AudioManager.instance.Play("Many", "button");
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.damnbadgames.space.poc.vs.angry.minions");
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1442689859");
#endif
        }
    }
}
