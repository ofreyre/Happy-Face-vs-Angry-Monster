using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : GameEnd
{
    void Start()
    {
        EventManager.instance.Win += OnEnd;
    }
}
