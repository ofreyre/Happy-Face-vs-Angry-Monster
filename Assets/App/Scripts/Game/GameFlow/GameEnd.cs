using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AudioManagement;

public class GameEnd : MonoBehaviour
{
    public GlobalFlow m_flow;
    public float m_delay = 3;
    public GameObject m_messageUI;
    public string m_nextScene;
    public string m_endSFX;

    protected void OnEnd()
    {
        AudioManager.instance.Play(m_endSFX);
        m_messageUI.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(m_delay);
        Score.instance.ToStats();
        Stars.instance.ToStats();
        m_flow.ToScene(m_nextScene);
    }
}
