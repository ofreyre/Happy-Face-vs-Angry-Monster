using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesListener : MonoBehaviour
{
    public EventManagerMessages m_eventManager;
    public MessageUI m_UI;

    // Use this for initialization
    void Start()
    {
        m_eventManager.Failure += m_UI.Failure;
        m_eventManager.Success += m_UI.Success;
    }
}
