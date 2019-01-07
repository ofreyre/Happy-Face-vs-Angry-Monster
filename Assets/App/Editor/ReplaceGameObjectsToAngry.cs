using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class ReplaceGameObjectsToAngry : EditorWindow
{

    public static void Display()
    {
        ReplaceGameObjectsToAngry instance = GetWindow<ReplaceGameObjectsToAngry>();
        instance.titleContent = new GUIContent("Replace GameObjects");
    }


    private void OnGUI()
    {
        if (GUILayout.Button("Replace"))
        {
            string[] gameObjects = new string[] { "GhostLightblue", "GhostPink", "GhostRed", "GhostYellow" };
            string[] prefabs = new string[] {
                "GhostLightblueAngry1", "GhostLightblueAngry2", "GhostLightblueAngry4", "GhostLightblueAngry8", "GhostLightblueAngry16",
                "GhostPinkAngry1", "GhostPinkAngry2", "GhostPinkAngry4", "GhostPinkAngry8", "GhostPinkAngry16",
                "GhostRedAngry1", "GhostRedAngry2", "GhostRedAngry4", "GhostRedAngry8", "GhostRedAngry16",
                "GhostYellowAngry1", "GhostYellowAngry2", "GhostYellowAngry4", "GhostYellowAngry8", "GhostYellowAngry16",
            };

            Dictionary<string, GameObject> newAssets = new Dictionary<string, GameObject>();

            //Lightblue
            //AssetDatabase.LoadAssetAtPath("Assets/SomeFolder/thePrefab.prefab", (typeof(GameObject))) as GameObject;

            string path = "Assets/App/Prefabs/Game/Ghosts/";
            string name;
            int n = prefabs.Length;
            for (int i = 0; i < n; i++)
            {
                name = prefabs[i];
                newAssets[name] = (GameObject)AssetDatabase.LoadAssetAtPath(path + name + ".prefab", typeof(GameObject));
            }

            Transform container = GameObject.Find("Enemies").transform;
            GameObject obj;
            Vector3 position;
            StartImpulseToTarget impulse;
            TaskRunOnStart taskRunOnStart;
            string prefabName;
            n = gameObjects.Length;
            int total = 0, matched = 0, found = 0, changed = 0;

            /*Transform temp = (new GameObject()).transform;
            temp.SetParent(container.parent);*/

            foreach (Transform ts in container)
            {
                total++;
                obj = null;

                for (int i = 0; i < n; i++)
                {
                    name = gameObjects[i];
                    if (ts.name.Contains(name))
                    {
                        matched++;
                        prefabName = name + "Angry" + ts.name[name.Length];
                        if (newAssets.ContainsKey(prefabName))
                        {
                            obj = Instantiate(newAssets[prefabName]);
                            obj.SetActive(ts.gameObject.activeSelf);
                            found++;
                        }
                        break;
                    }
                }

                if (obj != null)
                {
                    position = ts.position;
                    obj.transform.SetParent(container);
                    obj.transform.position = position;
                    impulse = ts.GetComponent<StartImpulseToTarget>();
                    obj.layer = ts.gameObject.layer;
                    if (impulse != null)
                    {
                        Debug.Log("StartImpulseToTarget");
                        ComponentUtility.CopyComponent(impulse);
                        ComponentUtility.PasteComponentAsNew(obj);
                    }
                    taskRunOnStart = ts.GetComponent<TaskRunOnStart>();
                    if (taskRunOnStart != null)
                    {
                        Debug.Log("TaskRunOnStart");
                        ComponentUtility.CopyComponent(taskRunOnStart);
                        ComponentUtility.PasteComponentAsNew(obj);
                    }

                    DestroyImmediate(ts.gameObject);
                    changed++;
                }
            }

            /*foreach (Transform ts in temp)
            {
                ts.SetParent(container);
            }*/

            //DestroyImmediate(temp.gameObject);

            Debug.Log("------------ Counts -----------");
            Debug.Log("Total: " + total);
            Debug.Log("Matched: " + matched);
            Debug.Log("Found: " + found);
            Debug.Log("Changed: " + changed);
            Debug.Log("-------------------------------");


            //Update Spawner
            Spawner.Prefabs[] m_prefabs = GameObject.Find("Controller").GetComponent<Spawner>().m_prefabs;
            m_prefabs[0].prefabs[0] = newAssets["GhostLightblueAngry16"];
            m_prefabs[0].prefabs[1] = newAssets["GhostLightblueAngry8"];
            n = m_prefabs.Length;
            for (int i = 0; i < n; i++) {
                for (int j = 0, m = m_prefabs[i].prefabs.Length - 1; j < m; j++) {
                    m_prefabs[i].prefabs[j] = newAssets[prefabs[i * 5 + m - j - 1]];
                }
            }
        }
    }
}
