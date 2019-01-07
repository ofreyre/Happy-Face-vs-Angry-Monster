using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour {
    
    public Text m_text;

    public virtual void Failure(string message) {
        m_text.text = message;
        gameObject.SetActive(true);
    }

    public virtual void Success(string message)
    {
        m_text.text = message;
        gameObject.SetActive(true);
    }
}
