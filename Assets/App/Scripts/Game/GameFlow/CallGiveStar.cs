using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGiveStar : TaskDelegate
{
    GameStats m_gameStats;
    public override void Delegate()
    {
        GetComponent<OnCollisionGiveStar>().enabled = true;
    }
}
