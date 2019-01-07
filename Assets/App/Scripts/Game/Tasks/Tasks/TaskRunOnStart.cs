using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRunOnStart : TaskRun
{
    public float force;
    void Start() {
        Run(force);
    }
}
