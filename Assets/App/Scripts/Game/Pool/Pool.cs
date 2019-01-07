using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    public GameObject m_prefab;
    public int m_min, m_max;
    public Stack<GameObject> m_objectsOff;
    public Exploder m_exploder;
    float m_upgradeShield;
    float m_upgradeStamina;
    float m_upgradeDamage;
    int m_count;
    GameObject local_gameObject = null;
    Shield local_shield;

    void Awake() {
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        //Debug.Log("Pool.Init "+name+" " + m_upgradeShield + " " + m_upgradeStamina + " " + m_upgradeDamage + " " + Time.time);
        //Debug.Log((m_objectsOff == null));
        if (m_objectsOff == null || m_objectsOff.Count < m_min)
        {
            m_objectsOff = new Stack<GameObject>();
            for (int i = m_objectsOff.Count; i < m_min; i++)
            {
                local_gameObject = Instantiate(m_prefab);
                local_gameObject.SetActive(false);
                //obj.transform.SetParent(transform);
                m_objectsOff.Push(local_gameObject);
                local_gameObject.GetComponent<PoolItem>().m_pool = this;

                local_shield = local_gameObject.GetComponent<Shield>();
                if (local_shield != null)
                {
                    local_shield.m_shield += m_upgradeShield;
                    local_shield.m_stamina += m_upgradeStamina;
                    local_shield.m_damage += m_upgradeDamage;
                    if (m_exploder != null)
                    {
                        local_shield.m_exploder = m_exploder;
                    }
                }
            }
            m_count = m_min;
        }
    }

    public GameObject Get(Exploder exploder = null) {
        local_gameObject = null;
        int n = m_objectsOff.Count;
        if (n > 0)
        {
            //Debug.Log("aaaa "+name);
            local_gameObject = m_objectsOff.Pop();
        }
        else if(m_count < m_max)
        {
            m_count++;
            //Debug.Log("bbbb " + name+"    m_min = "+m_min+" "+ m_count);
            local_gameObject = Instantiate(m_prefab);
            local_gameObject.GetComponent<PoolItem>().m_pool = this;

            local_shield = local_gameObject.GetComponent<Shield>();
            //obj.transform.SetParent(transform);

            if (local_shield != null)
            {
                local_shield.m_shield += m_upgradeShield;
                local_shield.m_stamina += m_upgradeStamina;
                local_shield.m_damage += m_upgradeDamage;
                if (exploder != null)
                {
                    local_shield.m_exploder = exploder;
                }
            }
        }
        return local_gameObject;
    }

    public void Return(GameObject gobj) {
        m_objectsOff.Push(gobj);
    }

    public void ApplyUpgrades(float shield, float stamina, float damage)
    {
        //Debug.Log("Pool.ApplyUpgrades "+ name+" "+shield+" "+ stamina+" "+ damage+" "+Time.time);
        m_upgradeShield = shield;
        m_upgradeStamina = stamina;
        m_upgradeDamage = damage;
    }
}
