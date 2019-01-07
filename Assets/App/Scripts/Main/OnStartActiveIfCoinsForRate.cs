using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartActiveIfCoinsForRate : MonoBehaviour {

    public GameObject m_target;
    
	void Start () {
        m_target.SetActive(DBmanager.CoinsForRate.amount>0);
	}
}
