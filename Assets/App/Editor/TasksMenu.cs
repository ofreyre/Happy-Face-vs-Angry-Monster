using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TasksMenu
{
    [MenuItem("Assets/Create/Tasks/Interval/Interval")]
    [MenuItem("Tasks/Interval/Interval")]
    public static void Task_Interval_new()
    {
        CreateAsset<TaskInterval>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Delay")]
    [MenuItem("Tasks/Interval/Delay")]
    public static void Task_Delay_new()
    {
        CreateAsset<TaskDelay>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Scale To")]
    [MenuItem("Tasks/Interval/Scale To")]
    public static void Task_ScaleTo_new()
    {
        CreateAsset<TaskScaleTo>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Scale From To")]
    [MenuItem("Tasks/Interval/Scale From To")]
    public static void Task_ScaleFromTo_new()
    {
        CreateAsset<TaskScaleFromTo>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Sprite blink")]
    [MenuItem("Tasks/Interval/Sprite blink")]
    public static void Task_SpriteBlink_new()
    {
        CreateAsset<TaskBlinkSprite>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Sprite color To")]
    [MenuItem("Tasks/Interval/Sprite color To")]
    public static void Task_ScaleColorTo_new()
    {
        CreateAsset<TaskSpriteColorTo>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Sprite color From To")]
    [MenuItem("Tasks/Interval/Sprite color From To")]
    public static void Task_ScaleColorFromTo_new()
    {
        CreateAsset<TaskSpriteColorFromTo>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Rotate To")]
    [MenuItem("Tasks/Interval/Rotate To")]
    public static void Task_RotateTo_new()
    {
        CreateAsset<TaskRotateTo>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Rotate From To")]
    [MenuItem("Tasks/Interval/Rotate From To")]
    public static void Task_RotateFromTo_new()
    {
        CreateAsset<TaskRotateFromTo>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Rotate random To")]
    [MenuItem("Tasks/Interval/Rotate random To")]
    public static void Task_RotateToRnd_new()
    {
        CreateAsset<TaskRotateToRnd>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Rotate random From To")]
    [MenuItem("Tasks/Interval/Rotate random From To")]
    public static void Task_RotateFromToRnd_new()
    {
        CreateAsset<TaskRotateFromToRnd>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Move")]
    [MenuItem("Tasks/Interval/Move")]
    public static void Task_Move_new()
    {
        CreateAsset<TaskMove>();
    }

    [MenuItem("Assets/Create/Tasks/Interval/Move to")]
    [MenuItem("Tasks/Interval/Move to")]
    public static void Task_MoveTo_new()
    {
        CreateAsset<TaskMoveTo>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Destroy")]
    [MenuItem("Tasks/Instant/Destroy")]
    public static void Task_Destroy_new()
    {
        CreateAsset<TaskDestroy>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Call delegate")]
    [MenuItem("Tasks/Instant/Destroy")]
    public static void Task_CallDelegate_new()
    {
        CreateAsset<TaskCallDelegate>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Set scale")]
    [MenuItem("Tasks/Instant/Set scale")]
    public static void Task_SetScale_new()
    {
        CreateAsset<TaskSetScale>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Impulse2D")]
    [MenuItem("Tasks/Instant/Set scale")]
    public static void Task_Impulse2D_new()
    {
        CreateAsset<TaskImpulse2D>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Set random rnd")]
    [MenuItem("Tasks/Instant/Set random rnd")]
    public static void Task_SetRotationRnd_new()
    {
        CreateAsset<TaskSetRotationRnd>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Set target active")]
    [MenuItem("Tasks/Instant/Set target active")]
    public static void Task_TargetSetActiveo_new()
    {
        CreateAsset<TaskTargetSetActive>();
    }

    [MenuItem("Assets/Create/Tasks/Instant/Run all tasks on target")]
    [MenuItem("Tasks/Instant/Run all tasks on target")]
    public static void Task_TargetRunAll_new()
    {
        CreateAsset<TaskTargetRunAll>();
    }

    [MenuItem("Assets/Create/Tasks/Composit/Sequence")]
    [MenuItem("Tasks/Composit/Sequence")]
    public static void Task_Sequence_new()
    {
        CreateAsset<TaskSequence>();
    }

    [MenuItem("Assets/Create/Tasks/Composit/Parallel")]
    [MenuItem("Tasks/Composit/Parallel")]
    public static void Task_Parallel_new()
    {
        CreateAsset<TaskParallel>();
    }

    public static T CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        return asset;
    }
}
