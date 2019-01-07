using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AudioManagement;

public enum USERVECTOR_TYPE{
    DragRelativeToTarget,
    DragRelativeToPoint,
    DragAccumulativeRelativeToTarget
}

[Serializable]
public struct USERVECTOR_TYPE_IngameContainer {
    public USERVECTOR_TYPE type;
    public GameObject gameObject;
}

public class UserVector : MonoBehaviour
{
    public USERVECTOR_TYPE type;
    public delegate void DelegateNewVector(Vector3 v);
    public static DelegateNewVector NewVector;
    public static DelegateNewVector NewVectorLapse;
    public float m_lapse = 0.1f;
    public float m_scale = 1;
    public float m_degreesOffset;
    public string m_shootSFX;
    protected ClipPlayer m_shootClipPlayer;
    protected bool m_audioOn;


    static bool m_registeredForEnd;

    protected virtual void Start()
    {
        if (!m_registeredForEnd)
        {
            m_registeredForEnd = true;
            EventManager.instance.Lose += ClearDelegates;
            m_shootClipPlayer = AudioManager.instance.Play(m_shootSFX, true);
            m_audioOn = m_shootClipPlayer != null;
            if (m_audioOn)
            {
                m_shootClipPlayer.Pause();
            }
        }
    }

    public static void ClearDelegates()
    {
        if (NewVector != null)
        {
            foreach (DelegateNewVector d in NewVector.GetInvocationList())
            {
                NewVector -= d;
            }
        }
        if (NewVectorLapse != null)
        {
            foreach (DelegateNewVector d in NewVectorLapse.GetInvocationList())
            {
                NewVectorLapse -= d;
            }
        }
        if (m_registeredForEnd)
        {
            EventManager.instance.End -= ClearDelegates;
            m_registeredForEnd = false;
        }
    }

    private void OnDestroy()
    {
        ClearDelegates();
    }

    private void OnDisable()
    {
        if (m_shootClipPlayer != null)
        {
            m_shootClipPlayer.Pause();
        }
    }
}
