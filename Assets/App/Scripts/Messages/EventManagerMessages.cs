using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerMessages : MonoBehaviour
{
    public delegate void DelegateMessage(string message);
    public DelegateMessage Failure;
    public DelegateMessage Success;
    public delegate void DelegateInt(int value);
    public DelegateInt Int;

    public static EventManagerMessages instance;

    private void Awake()
    {
        instance = this;
    }

    public void DispatchFailure(string message)
    {
        if (Failure != null)
        {
            Failure(message);
        }
    }

    public void DispatchSuccess(string message)
    {
        if (Success != null)
        {
            Success(message);
        }
    }

    public void DispatchInt(int value)
    {
        if (Int != null)
        {
            Int(value);
        }
    }
}
