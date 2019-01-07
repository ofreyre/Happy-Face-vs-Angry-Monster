using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverAnimation : MonoBehaviour {

    public Text m_text;
    public float m_invisibleDuration = 0.1f;
    public float m_visibleDuration = 0.5f;

    // Use this for initialization
    void Start () {
        StartCoroutine(Blink());		
	}

    IEnumerator Blink() {
        while (true)
        {
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 1);
            yield return new WaitForSeconds(m_visibleDuration);
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 0);
            yield return new WaitForSeconds(m_invisibleDuration);
        }
    }
}
