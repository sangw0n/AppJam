using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitEvent : MonoBehaviour
{
    [SerializeField]
    private   int _weight = 1;
    public int Weight => _weight;
    
    [SerializeField]
    protected string[] _commentary;
    protected Unit _myUnit;

    public void SetOwner(Unit owner)
    {
        _myUnit = owner;
    }

    public abstract void Init();
    public abstract void InvokeEvent();
    public abstract bool IsEnd();

}
