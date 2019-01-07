using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTranslate : MonoBehaviour//, IEffect3
{
    public float speed;
    public float m_duration = 3;
    public bool m_disactivateAtEnd;
    public bool m_lookAtVelocity;

    void Start() {
    }

    public void Init()
    {

    }

    public void Run(Vector3 force, float duration)
    {
        if (m_lookAtVelocity) {
            float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        gameObject.SetActive(true);
        StartCoroutine(_Run(force, duration));
    }

    //Force has to be normalized
    IEnumerator _Run(Vector3 force, float duration)
    {
        float t1 = Time.time + duration;
        //force *= speed;
        while (t1 > Time.time)
        {
            transform.position += force * Time.deltaTime;
            yield return null;
        }
        if (m_disactivateAtEnd) {
            gameObject.SetActive(false);
        }
    }
}
