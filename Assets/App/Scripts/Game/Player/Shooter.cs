using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public INVENTORYITEM_TYPE m_itemType;
    public Pool m_missiles;
    public Exploder m_exploder;
    public int m_amount;
    public bool m_parallel = true;
    public float m_speed;
    public float m_duration;
    Transform[] m_shootPoints;
    int m_shootPointsCount;
    int m_shootPointI;
    List<Transform> local_points = new List<Transform>();
    Transform local_transform;
    GameObject local_gameObject;

    // Use this for initialization

    void Awake()
    {
        m_missiles.m_exploder = m_exploder;
    }

    void Start ()
    {
        UserVector.NewVectorLapse -= Shoot;
        UserVector.NewVectorLapse += Shoot;
        /*
        List<GameObject> objectsOff = m_missiles.m_objectsOff;
        Shield shield;
        for (int i = 0, n = objectsOff.Count; i < n; i++) {
            shield = objectsOff[i].GetComponent<Shield>();
            if (shield != null) {
                shield.m_exploder = m_exploder;
            }
        }*/
    }

    public void SetWeapon(Transform weapon) {
        local_points.Clear();
        foreach (Transform ts in weapon) {
            local_points.Add(ts);
        }
        m_shootPoints = local_points.ToArray();
        m_shootPointsCount = m_shootPoints.Length;
        m_shootPointI = 0;
    }

    void Shoot(Vector3 direction) {
        if (direction != Vector3.zero)
        {
            if (m_parallel)
            {
                for (int i = 0; i < m_shootPointsCount; i++)
                {
                    local_transform = m_shootPoints[i];
                    //if (!_Shoot(direction, shooterPoint.point.position, shooterPoint.rotationDegs))
                    if (!_Shoot(local_transform.up, local_transform.position))
                    {
                        EventManager.instance.DispatchAmmoExhausted();
                        return;
                    }
                }
            }
            else
            {
                local_transform = m_shootPoints[m_shootPointI];
                //_Shoot(direction, shooterPoint.point.position, shooterPoint.rotationDegs);
                if (!_Shoot(local_transform.up, local_transform.position))
                {
                    EventManager.instance.DispatchAmmoExhausted();
                }
                m_shootPointI = (m_shootPointI + 1) % m_shootPointsCount;
            }
        }
    }

    bool _Shoot(Vector3 direction, Vector3 position)
    {
        local_gameObject = m_missiles.Get(m_exploder);
        if (local_gameObject != null)
        {
            local_gameObject.transform.parent = null;
            //direction.Normalize();
            //direction = Quaternion.AngleAxis(rotationDegs, Vector3.up) * direction * m_speed;
            direction = direction.normalized * m_speed;
            local_gameObject.transform.position = position;
            local_gameObject.GetComponent<EffectTranslate>().Run(direction, m_duration);
            if (m_amount > 0)
            {
                m_amount--;
                Ammo.instance.value = m_amount;
                if (m_amount < 1)
                {
                    m_amount = 0;
                    return false;
                }
            }
            m_shootPointI = (m_shootPointI + 1) % m_shootPointsCount;
            return true;
        }
        return true;
    }

    public void AddAmount(int amount) {
        m_amount += amount;
    }

    public void Activate(int amount, Transform weapon)
    {
        if (amount > -1)
        {
            m_amount += amount;
            Ammo.instance.value = m_amount;
        }
        else
        {
            //m_amount = amount;
            Ammo.instance.text = "infinite";
        }
        SetWeapon(weapon);
        gameObject.SetActive(true);
        UserVector.NewVectorLapse -= Shoot;
        UserVector.NewVectorLapse += Shoot;
    }

    public void Disactivate() {
        UserVector.NewVectorLapse -= Shoot;
        gameObject.SetActive(false);
    }

    public void ApplyUpgrades(float speed, float duration, float shield, float stamina, float damage)
    {
        m_speed += speed;
        m_duration += duration;
        m_missiles.ApplyUpgrades(shield, stamina, damage);

    }
}
