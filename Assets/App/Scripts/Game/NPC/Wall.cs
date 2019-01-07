using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public enum SIDE
    {
        left,
        right,
        top,
        bottom
    }

    public float m_force;
    public Vector2 m_bounce;
    public float m_bounceK = 1;
    public float m_bounceMin = 0.1f;
    public SIDE m_side;
    Geometry local_geometry;

    void OnCollisionEnter2D(Collision2D col)
    {
        local_geometry = col.gameObject.GetComponent<Geometry>();
        if (local_geometry != null)
        {
            Vector2 velocity = GetBounce(col.relativeVelocity) * local_geometry.bounceK * Scroller.instance.m_speed * m_bounceK;
            local_geometry.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }

    Vector2 GetNormal() {
        switch (m_side)
        {
            case SIDE.left:
                return new Vector2(1, 0);
            case SIDE.right:
                return new Vector2(-1, 0);
            case SIDE.top:
                return new Vector2(0, -1);
            default:
                return new Vector2(0, 1);
        }
    }

    Vector2 GetBounce(Vector2 v)
    {
        v = new Vector2(v.x * m_bounce.x, v.y * m_bounce.y).normalized;
        switch (m_side)
        {
            case SIDE.left:
                if (v.y == 0) {
                    return new Vector2(1, Random.Range(-m_bounceK, m_bounceK));
                }
                return new Vector2(Mathf.Max(Mathf.Abs(m_bounceMin * v.y), Mathf.Abs(v.x)), v.y).normalized;
            case SIDE.right:
                if (v.y == 0)
                {
                    return new Vector2(-1, Random.Range(-m_bounceK, m_bounceK));
                }
                return new Vector2(-Mathf.Max(Mathf.Abs(m_bounceMin * v.y), Mathf.Abs(v.x)), v.y).normalized;
            case SIDE.top:
                if (v.x == 0)
                {
                    return new Vector2(Random.Range(-m_bounceK, m_bounceK), -1);
                }
                return new Vector2(v.x, -Mathf.Max(Mathf.Abs(m_bounceMin * v.x), Mathf.Abs(v.y))).normalized;
            default:
                if (v.x == 0)
                {
                    return new Vector2(Random.Range(-m_bounceK, m_bounceK), 1);
                }
                return new Vector2(v.x, Mathf.Max(Mathf.Abs(m_bounceMin * v.x), Mathf.Abs(v.y))).normalized;
        }

    }
}
