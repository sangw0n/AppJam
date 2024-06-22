using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnitEvent unityEvent;

    private void Start()
    {
        unityEvent.InvokeEvent();
    }
}
