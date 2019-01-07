using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetActive : MonoBehaviour {

    public GameObject m_target;

    public void OnClick(bool active) {
        m_target.SetActive(active);
    }
}
