using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillLevelsSettings : EditorWindow
{

    public static void Display()
    {
        FillLevelsSettings instance = GetWindow<FillLevelsSettings>();
        instance.titleContent = new GUIContent("Fill Levels Settings");
    }


    private void OnGUI()
    {
        if (GUILayout.Button("Fill"))
        {
            LevelsSettings settings = (LevelsSettings)AssetDatabase.LoadAssetAtPath<LevelsSettings>("Assets/App/Data/LevelsSettings.asset");
            SerializedObject so = new SerializedObject(settings);
            so.Update();
            SerializedProperty levels = so.FindProperty("levels");
            for (int i = 10, n = settings.levels.Length; i < n; i++) {
                levels.GetArrayElementAtIndex(i).FindPropertyRelative("wallSides").FindPropertyRelative("bounceK").floatValue =
                    settings.levels[i%10].wallSides.bounceK;
                levels.GetArrayElementAtIndex(i).FindPropertyRelative("wallSides").FindPropertyRelative("bounceMin").floatValue =
                    settings.levels[i % 10].wallSides.bounceK;
                levels.GetArrayElementAtIndex(i).FindPropertyRelative("wallBottom").FindPropertyRelative("bounceK").floatValue =
                    settings.levels[i % 10].wallSides.bounceK;
                levels.GetArrayElementAtIndex(i).FindPropertyRelative("wallBottom").FindPropertyRelative("bounceMin").floatValue =
                    settings.levels[i % 10].wallSides.bounceK;
                levels.GetArrayElementAtIndex(i).FindPropertyRelative("wallTop").FindPropertyRelative("bounceK").floatValue =
                    settings.levels[i % 10].wallSides.bounceK;
                levels.GetArrayElementAtIndex(i).FindPropertyRelative("wallTop").FindPropertyRelative("bounceMin").floatValue =
                    settings.levels[i % 10].wallSides.bounceK;
            }

            so.ApplyModifiedProperties();
            //AssetDatabase.ForceReserializeAssets(new string[] { "Assets/Data/LevelsSettings.asset" }, ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();

        }
    }
}
