using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillScriptsTarget : EditorWindow
{
    public static void Display()
    {
        FillScriptsTarget instance = GetWindow<FillScriptsTarget>();
        instance.titleContent = new GUIContent("Fill Scripts targets");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Fill"))
        {
            Transform container = GameObject.Find("Enemies").transform;
            Transform pacman = GameObject.Find("Pacman").transform;
            StartImpulseToTarget impulse;
            EnemyAroundS enemyS;
            Geometry[] geometries;
            Geometry geometry;
            SerializedObject so;
            string name;
            foreach (Transform ts in container)
            {
                impulse = ts.GetComponent<StartImpulseToTarget>();
                if (impulse != null)
                {
                    so = new SerializedObject(impulse);
                    so.Update();
                    so.FindProperty("m_target").objectReferenceValue = pacman;
                    so.ApplyModifiedProperties();
                }
                enemyS = ts.GetComponent<EnemyAroundS>();
                if (enemyS != null)
                {
                    so = new SerializedObject(enemyS);
                    so.Update();
                    so.FindProperty("m_target").objectReferenceValue = pacman;


                    name = ts.GetComponent<SpriteRenderer>().sprite.name;
                    switch (name)
                    {
                        case "GhostLightblueAngry":
                            so.FindProperty("prefabsIndex").intValue = 4;
                            break;
                        case "GhostPinkAngry":
                            so.FindProperty("prefabsIndex").intValue = 5;
                            break;
                        case "GhostRedAngry":
                            so.FindProperty("prefabsIndex").intValue = 6;
                            break;
                        case "GhostYellowAngry":
                            so.FindProperty("prefabsIndex").intValue = 7;
                            break;
                    }

                    so.ApplyModifiedProperties();

                    geometries = ts.GetComponents<Geometry>();
                    if (geometries.Length > 1)
                    {
                        foreach (Geometry g in geometries)
                        {
                            if (g != enemyS)
                            {
                                DestroyImmediate(g);
                                Debug.Log(ts.name);
                            }
                        }
                    }

                    if (ts.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Kinematic)
                    {
                        Debug.Log(ts.name);
                    }
                }
                else {
                    geometry = ts.GetComponent<Geometry>();
                    if (geometry != null)
                    {
                        so = new SerializedObject(geometry);
                        so.Update();
                        name = ts.GetComponent<SpriteRenderer>().sprite.name;
                        switch (name)
                        {
                            case "GhostLightblueAngry":
                                so.FindProperty("prefabsIndex").intValue = 4;
                                break;
                            case "GhostPinkAngry":
                                so.FindProperty("prefabsIndex").intValue = 5;
                                break;
                            case "GhostRedAngry":
                                so.FindProperty("prefabsIndex").intValue = 6;
                                break;
                            case "GhostYellowAngry":
                                so.FindProperty("prefabsIndex").intValue = 7;
                                break;
                        }
                        so.ApplyModifiedProperties();
                    }
                }
            }

            container = container.Find("Collectables");
            foreach (Transform ts in container)
            {
                impulse = ts.GetComponent<StartImpulseToTarget>();
                if (impulse != null)
                {
                    so = new SerializedObject(impulse);
                    so.Update();
                    so.FindProperty("m_target").objectReferenceValue = pacman;
                    so.ApplyModifiedProperties();
                }
            }
        }
    }
}
