using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnPlay : MonoBehaviour {

    public GameObject m_playUI;

    public void OnCLick(bool play) {
        Time.timeScale = play ? 1 : 0;
        m_playUI.SetActive(!play);
        GameObject.Find("Controller").transform.Find("UserInputs").gameObject.SetActive(play);
    }
}
