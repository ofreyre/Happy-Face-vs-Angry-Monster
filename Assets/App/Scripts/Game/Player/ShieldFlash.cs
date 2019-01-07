using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFlash : MonoBehaviour, IEffect
{
    public ShieldFlashRay m_ray1;
    public ShieldFlashRay m_ray2;
    public ShieldFlashRay m_ray3;
    public float m_flash2startTime = 0.1f;
    public float m_flash3startTime = 0.1f;
    public int m_finished = 3;
    WaitForSeconds m_flashWait;

    void Start()
    {
        m_flashWait = new WaitForSeconds(m_flash2startTime);
        m_ray1.Init();
        m_ray2.Init();
        m_ray3.Init();
    }

    public void Init() {

    }

    public void Run(float force)
    {
        if (m_finished > 2)
        {
            m_finished = 0;
            StartCoroutine(_Run(force));
        }
    }

    IEnumerator _Run(float force)
    {
        m_ray1.Run(force);
        yield return m_flashWait;
        m_ray2.Run(force);
        yield return m_flashWait;
        m_ray3.Run(force);
    }

    public void RayFinished() {
        m_finished++;
    }
}
