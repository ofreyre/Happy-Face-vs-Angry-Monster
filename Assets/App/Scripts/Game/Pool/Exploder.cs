using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class Exploder : MonoBehaviour
{
    public Pool m_explosions;
    public string m_explodeSFX;
    AudioManager m_audioManager;
    Destructible local_destructible;
    Geometry local_geometry;
    GameObject local_gameObject;
    ContactPoint2D[] local_contacts;

    private void Start()
    {
        local_contacts = new ContactPoint2D[1];
        m_audioManager = AudioManager.instance;
    }

    public void Explode(Collision2D col, float damage, Vector3 position)
    {
        local_destructible = col.gameObject.GetComponent<Destructible>();
        if (local_destructible != null)
        {
            col.GetContacts(local_contacts);
            Rigidbody2D rig = local_destructible.GetComponent<Rigidbody2D>();
            //Vector2 impulse = contacts[0].normal * rig.velocity.magnitude * rig.mass + (new Vector2(m_pos.x - m_prevPos.x, m_pos.y - m_prevPos.y)) / m_deltaTime * rig.mass;
            Vector2 v = local_contacts[0].point - new Vector2(local_destructible.transform.position.x, local_destructible.transform.position.y);
            float dot = Vector2.Dot(v, -local_contacts[0].normal);
            Vector2 velocity;
            if (dot == 0)
            {
                velocity = -local_contacts[0].normal * local_destructible.bounceK * Scroller.instance.m_speed * local_destructible.m_destructionForce;
            }
            else
            {
                velocity = (2 * dot / v.sqrMagnitude * v + v).normalized * local_destructible.bounceK * Scroller.instance.m_speed * local_destructible.m_destructionForce;
            }
            bool dead = local_destructible.Damage(damage);
            float enemyDamage = local_destructible.GetDamageResult(damage);
            Vector3 pos = position.x == Mathf.Infinity ? local_destructible.transform.position : position;
            if (dead)
            {
                if (local_destructible.m_points != 0)
                {
                    Score.instance.Add(local_destructible.m_points, col.transform.position);
                }
                RunTasks(enemyDamage * 2, pos);
                local_geometry = col.gameObject.GetComponent<Geometry>();
                if (local_geometry != null)
                {
                    Spawner.instance.Divide(
                        local_geometry,
                        velocity
                        );
                }
                /*if (m_shieldFlash != null)
                {
                    m_shieldFlash.Run(m_damage);
                }*/
            }
            else
            {
                RunTasks(enemyDamage, pos);
                if (local_destructible.m_destructionForce != 0 && rig != null)
                {
                    rig.velocity = velocity;
                }
            }
        }
    }

    public void Explode(Transform colliderTransform, float damage, Vector3 position, Vector3 direction)
    {
        local_destructible = colliderTransform.gameObject.GetComponent<Destructible>();
        if (local_destructible != null)
        {
            Rigidbody2D rig = local_destructible.GetComponent<Rigidbody2D>();
            //Vector2 impulse = contacts[0].normal * rig.velocity.magnitude * rig.mass + (new Vector2(m_pos.x - m_prevPos.x, m_pos.y - m_prevPos.y)) / m_deltaTime * rig.mass;
            Vector2 normal = new Vector2(colliderTransform.transform.position.x, colliderTransform.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            float dot = Vector2.Dot(direction, normal);
            Vector2 velocity;
            if (dot == 0)
            {
                velocity = normal * local_destructible.bounceK * Scroller.instance.m_speed;
            }
            else
            {
                velocity = (2 * dot / direction.sqrMagnitude * direction + direction).normalized * local_destructible.bounceK * Scroller.instance.m_speed;
            }
            bool dead = local_destructible.Damage(damage);
            float enemyDamage = local_destructible.GetDamageResult(damage);
            Vector3 pos = position.x == Mathf.Infinity ? local_destructible.transform.position : position;
            if (dead)
            {
                if (local_destructible.m_points != 0)
                {
                    Score.instance.Add(local_destructible.m_points, colliderTransform.position);
                }
                RunTasks(enemyDamage * 2, pos);
                local_geometry = colliderTransform.gameObject.gameObject.GetComponent<Geometry>();
                if (local_geometry != null)
                {
                    Spawner.instance.Divide(
                    local_geometry,
                    velocity
                    );
                }
                /*if (m_shieldFlash != null)
                {
                    m_shieldFlash.Run(m_damage);
                }*/
            }
            else
            {
                RunTasks(enemyDamage, pos);
                rig.velocity = velocity;
            }
        }
    }

    void RunTasks(float damage, Vector3 position)
    {
        m_audioManager.Play(m_explodeSFX);
        local_gameObject = m_explosions.Get();
        if (local_gameObject != null)
        {
            local_gameObject.transform.parent = null;
            local_gameObject.transform.position = position;
            TaskRun[] tasks = local_gameObject.GetComponents<TaskRun>();
            for (int i = 0, n = tasks.Length; i < n; i++)
            {
                tasks[i].Run(damage);
            }
        }
    }
}
