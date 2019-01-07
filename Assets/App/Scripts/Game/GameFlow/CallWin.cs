using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWin : TaskDelegate
{
    public override void Delegate()
    {
        GetComponent<OnCollisionGiveStar>().enabled = true;
        EventManager.instance.DispatchWin();
    }
}
