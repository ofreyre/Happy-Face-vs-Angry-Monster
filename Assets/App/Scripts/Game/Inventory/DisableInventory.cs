using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInventory : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.instance.End += Disable;
        EventManager.instance.Win += Disable;
    }
	
	// Update is called once per frame
	void Disable() {
        gameObject.SetActive(false);
	}
}
