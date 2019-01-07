using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public class ButtonRate : MonoBehaviour
{
    public void OnClick()
    {
        //AudioManager.instance.Play("Many", "button");
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.damnbadgames.space.pacman.vs.ghost.minions");
#elif UNITY_IPHONE
			int systemversion = int.Parse(Device.systemVersion.Split('.')[0]);
			if(systemversion<7){
				Application.OpenURL("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=1107187117");
			}else if(systemversion==7){
				Application.OpenURL("itms-apps://itunes.apple.com/app/id1107187117");
			}else{
				Application.OpenURL("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=1107187117&onlyLatestVersion=true&pageNumber=0&sortOrdering=1&type=Purple+Software");
			}
#endif
    }
}
