using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class Victim : Destructible, IExploderContainer
{
    public Exploder m_exploder;
    public GameObject m_character;
    public float m_rotationObset;
    public float m_upgradeStaminaCollected;
    public string m_collectSFX;
    Geometry local_geometry;

    public Exploder exploder
    {
        get { return m_exploder; }
        set { m_exploder = value; }
    }

    // Use this for initialization
    void Start () {
        m_stamina = Stamina.instance.m_value;
        UserVector.NewVector += LookAt;
        EventManager.instance.StaminaCollected += OnStaminaCollected;
        EventManager.instance.ChargeStamina += ChargeStamina;
        EventManager.instance.Win += OnWin;
    }

    public void ApplyUpgrades(float shield, float stamina, float damage, float staminaCollected) {
        m_shield = shield;
        m_stamina = stamina;
        m_damage = damage;
        m_upgradeStaminaCollected = staminaCollected;
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        local_geometry = col.gameObject.GetComponent<Geometry>();
        if (local_geometry != null)
        {

            float damage = GetDamageResult(local_geometry.m_damage) * (1 - m_shield);
            m_stamina -= damage;
            m_exploder.Explode(col, m_damage, col.transform.position);
            Stamina.instance.value = m_stamina;

            Tremor.instance.Shake(damage);
            if (m_stamina < 0)
            {
                Stamina.instance.value = 0;
                EventManager.instance.DispatchLose();
                GetComponent<Collider2D>().enabled = false;
                //GetComponent<UserVector>().enabled = false;
                EventManager.instance.End += OnEnd;
                m_character.GetComponent<Animator>().SetTrigger("die");
            }
        }
    }

    void OnStaminaCollected(CollectableStamina collectable)
    {
        AddStamina(collectable.m_amount);
    }

    void ChargeStamina(DBinventoryItem item) {
        if (item.type != INVENTORYITEM_TYPE.cherry)
        {
            AddStamina(item.amount);
        }
        else {
            AddStaminaMax(item.amount);
        }
    }

    void AddStamina(float amount)
    {
        float stamina = Stamina.instance.m_value;
        float staminaMax = Stamina.instance.m_valueMax;
        AudioManager.instance.Play(m_collectSFX);
        if (stamina < staminaMax)
        {
            m_stamina = Mathf.Min(staminaMax, stamina + amount + m_upgradeStaminaCollected);
            Stamina.instance.value = m_stamina;
        }
    }

    void AddStaminaMax(float amount) {
        Stamina.instance.ValueMax += amount;
        Stamina.instance.Refresh();
    }

    void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + m_rotationObset;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnEnd() {
        gameObject.SetActive(false);
    }

    void OnWin() {
        GetComponent<Collider2D>().enabled = false;
    }
}
