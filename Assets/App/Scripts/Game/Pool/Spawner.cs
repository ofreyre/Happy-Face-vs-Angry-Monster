using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AudioManagement;

public class Spawner : MonoBehaviour {

    [Serializable]
    public struct Prefabs
    {
        public GameObject[] prefabs;
        public float[] divideDistance;
        public float[] divideImpulseK;
    }

    [Serializable]
    public struct PrefabsCount
    {
        public int[] m_minObjects;
        public int[] m_maxObjects;
    }
    
    public Prefabs[] m_prefabs;
    public PrefabsCount[] m_prefabsCounts;
    [SerializeField]
    string m_dieSFX;
    AudioManager m_audioManager;

    Stack<GameObject>[][] m_enemiesOff;

    Vector3[] m_dividePositions = { new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0), new Vector3(-1, 1) };
    
    public static Spawner instance;
    public Transform m_enemies;
    GameObject local_obj;
    Geometry local_geometry;
    GameObject[] local_objs;
    Prefabs local_prefs;
    Stack<GameObject> local_enemiesOff;
    int m_getEnemiesCount;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        m_audioManager = AudioManager.instance;
        
        int[] minObjects;
        GameObject prefab;
        m_enemiesOff = new Stack<GameObject>[m_prefabsCounts.Length][];
        for (int i = 0, n = m_prefabsCounts.Length; i < n; i++) {
            minObjects = m_prefabsCounts[i].m_minObjects;
            if (minObjects != null)
            {
                m_enemiesOff[i] = new Stack<GameObject>[minObjects.Length];
                local_objs = m_prefabs[i].prefabs;
                for (int j = 0, m = minObjects.Length; j < m; j++)
                {
                    prefab = local_objs[j];
                    local_enemiesOff = new Stack<GameObject>();
                    m_enemiesOff[i][j] = local_enemiesOff;
                    for (int k = 0, p = minObjects[j]; k < p; k++)
                    {

                        local_obj = Instantiate(prefab);
                        prefab.SetActive(false);
                        local_geometry = local_obj.GetComponent<Geometry>();
                        if (local_geometry == null)
                        {
                            local_geometry = local_obj.AddComponent<Geometry>();
                        }
                        local_geometry.prefabsIndex = i;
                        local_geometry.prefabIndex = j;
                        local_enemiesOff.Push(local_obj);
                        local_obj.transform.SetParent(m_enemies);
                    }
                }
            }
        }
        local_objs = new GameObject[4];
    }

    public void Divide(Geometry geom, Vector2 velocity)
    {
        local_prefs = m_prefabs[geom.prefabsIndex];
        //Debug.Log(geom.prefabsIndex);
        if (geom.prefabIndex < local_prefs.prefabs.Length - 1)
        {
            float impulseK = local_prefs.divideImpulseK[geom.prefabIndex + 1];
            //GameObject prefab = prefs.prefabs[geom.prefabIndex + 1];
            float distance = local_prefs.divideDistance[geom.prefabIndex + 1];
            if (geom.prefabIndex < local_prefs.prefabs.Length - 2)
            {
                //local_objs = GetEnemies(geom.prefabsIndex, geom.prefabIndex + 1, 4);
                GetEnemies(geom.prefabsIndex, geom.prefabIndex + 1, 4);
                for (int i = 0; i < m_getEnemiesCount; i++)
                {
                    //local_obj = local_objs[i];
                    //if (local_obj != null)
                    {
                        Spawn(local_objs[i], geom.transform, distance, m_dividePositions[i], velocity, impulseK);
                    }
                    /*else {
                        break;
                    }*/
                }
            }
            else
            {
                m_audioManager.Play(m_dieSFX);
                //local_obj = GetEnemies(geom.prefabsIndex, geom.prefabIndex + 1, 1)[0];
                GetEnemies(geom.prefabsIndex, geom.prefabIndex + 1, 1);
                //if (local_obj != null)
                if(m_getEnemiesCount > 0)
                {
                    //Spawn(local_obj, geom.transform, distance, Vector3.zero, velocity, impulseK, geom.transform.localScale.x);
                    Spawn(local_objs[0], geom.transform, distance, Vector3.zero, velocity, impulseK, geom.transform.localScale.x);
                }
            }
        }
        if (geom.m_preserve)
        {
            RemoveEnemy(geom.prefabsIndex, geom.prefabIndex, geom.gameObject);
        }
        else {
            Destroy(geom.gameObject);
        }
    }

    void Spawn(GameObject obj, Transform collided, float distance, Vector3 relativePosition, Vector2 velocity, float impulseK, float scale = 1)
    {
        Vector3 scaleV = obj.transform.localScale * scale;
        obj.transform.SetParent(collided, false);
        obj.transform.localPosition = relativePosition * distance;
        obj.transform.SetParent(m_enemies);
        obj.transform.localScale = scaleV;
        obj.GetComponent<Rigidbody2D>().velocity = velocity + new Vector2(relativePosition.x, relativePosition.y) * impulseK;
    }

    //GameObject[] GetEnemies(int prefabsIndex, int prefabIndex, int amount)
    void GetEnemies(int prefabsIndex, int prefabIndex, int amount)
    {
        //local_objs = new GameObject[amount];
        local_enemiesOff = m_enemiesOff[prefabsIndex][prefabIndex];

        int n = local_enemiesOff.Count-1;
        for (m_getEnemiesCount = 0; m_getEnemiesCount < amount; m_getEnemiesCount++) {
            if (n - m_getEnemiesCount > -1)
            {
                local_obj = local_enemiesOff.Pop();//enemiesOff[n - i];
                //enemiesOff.RemoveAt(n - i);
                local_objs[m_getEnemiesCount] = local_obj;
                local_obj.SetActive(true);
            }
            else {
                return;
                //return local_objs;
                local_obj = Instantiate(m_prefabs[prefabsIndex].prefabs[prefabIndex]);
                local_geometry = local_obj.GetComponent<Geometry>();
                if (local_geometry == null)
                {
                    local_geometry = local_obj.AddComponent<Geometry>();
                }
                local_geometry.prefabsIndex = prefabsIndex;
                local_geometry.prefabIndex = prefabIndex;
                local_obj.transform.SetParent(m_enemies);
                local_obj.SetActive(true);
                local_objs[m_getEnemiesCount] = local_obj;
            }
        }
        //return local_objs;
    }

    public void RemoveEnemy(int prefabsIndex, int prefabIndex, GameObject obj)
    {
        m_enemiesOff[prefabsIndex][prefabIndex].Push(obj);
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
    }
}
