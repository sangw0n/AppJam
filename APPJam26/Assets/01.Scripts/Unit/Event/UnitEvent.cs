using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitEvent : MonoBehaviour
{

    protected Unit _myUnit;

    public void SetOwner(Unit owner)
    {
        _myUnit = owner;
    }

    public abstract void InvokeEvent();

}
