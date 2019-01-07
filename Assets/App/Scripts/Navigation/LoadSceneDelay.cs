using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDelay : MonoBehaviour {

    public float m_delay = 3;
    public string m_scene = "Main"; 

	// Use this for initialization
	void Start () {
        StartCoroutine(Load());
	}
	
	IEnumerator Load () {
        yield return new WaitForSeconds(m_delay);
        SceneManager.LoadScene(m_scene);
    }
}
