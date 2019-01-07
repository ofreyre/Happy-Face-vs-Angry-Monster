using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public INVENTORYITEM_TYPE m_type;
    public Exploder m_exploder;
    public float m_delay = 0.1f;
    public float m_lapse = 0.2f;
    public float m_damageUpgrade;
    public float m_durationUpgrade;
    public float m_radiusUpgrade;
    WaitForSeconds m_wait;
    Collider2D[] local_colliders;
    Collider2D local_colider;

    public void Explode(float damage, float radius, float duration)
    {
        if (m_wait == null) {
            m_wait = new WaitForSeconds(m_lapse);
        }
        radius += m_radiusUpgrade;
        float scale = radius * 2 / GetComponent<SpriteRenderer>().size.x;
        transform.localScale = scale * Vector3.one;
        duration += m_durationUpgrade;
        GetComponent<TaskRunOnEnable>().force = duration;
        gameObject.SetActive(true);
        damage += m_damageUpgrade;
        StartCoroutine(_Explode(damage, radius, duration));
    }

    IEnumerator _Explode(float damage, float radius, float duration) {
        yield return m_wait;
        while (true) {
            local_colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
            int n = local_colliders.Length;
            for (int i = 0; i < n; i++)
            {
                local_colider = local_colliders[i];
                Geometry geometry = local_colider.gameObject.GetComponent<Geometry>();
                if (geometry != null)
                {
                    m_exploder.Explode(local_colider.transform, damage, local_colider.transform.position, local_colider.transform.position - transform.position);
                }
            }
            yield return m_wait;
        }
    }
}
