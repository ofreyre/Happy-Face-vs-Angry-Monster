using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveIfStars : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DBupgrades dbUpgrades = DBmanager.Upgrades;
        gameObject.SetActive(DBmanager.Stars - dbUpgrades.TotalStars > 0);
    }
}
