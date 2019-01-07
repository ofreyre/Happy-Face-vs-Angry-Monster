using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loser : GameEnd
{

    void Start ()
    {
        EventManager.instance.End += OnEnd;
    }
}
