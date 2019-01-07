using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class BtnPlaySFX : MonoBehaviour {

    public string m_clip;

    public void Play() {
        AudioManager.instance.Play(m_clip);
    }
}
