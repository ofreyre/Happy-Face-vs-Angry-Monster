using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    ITask Instantiate();
    ITask Init(Transform transform, float forcel, bool first = true);
    bool Run();
    ITask GetNext();
}
