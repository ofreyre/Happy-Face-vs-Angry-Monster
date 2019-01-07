using UnityEditor;

namespace DBGads
{
    public class DBGadsEditor
    {
        [MenuItem("DBGads/New Banner")]
        [MenuItem("Assets/DBGads/New Banner")]
        public static void Banner_new()
        {
            UtilsScriptableObject.CreateAsset<AdBannerManager>();
        }

        [MenuItem("DBGads/New Interstitial")]
        [MenuItem("Assets/DBGads/New Interstitial")]
        public static void Interstitial_new()
        {
            UtilsScriptableObject.CreateAsset<AdInterstitialManager>();
        }

        [MenuItem("DBGads/New Video")]
        [MenuItem("Assets/DBGads/New Video")]
        public static void Video_new()
        {
            UtilsScriptableObject.CreateAsset<AdVideoManager>();
        }

        [MenuItem("DBGads/New Manager")]
        [MenuItem("Assets/DBGads/New Manager")]
        public static void AdManager_new()
        {
            UtilsScriptableObject.CreateAsset<AdManager>();
        }

        [MenuItem("DBGads/Delete DB")]
        [MenuItem("Assets/DBGads/Delete DB")]
        public static void DeleteDB_new()
        {
            AdSDB.Delete();
        }
    }
}
