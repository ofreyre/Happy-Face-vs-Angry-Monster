using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRunOnEnable : TaskRun
{
    public float force;
    void OnEnable() {
        Run(force);
    }
}
