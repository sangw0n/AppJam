using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUnitEvent : UnitEvent
{
    bool _isEnd = false;

    public override void Init()
    {
        _isEnd = false;
    }

    public override void InvokeEvent()
    {
        StartCoroutine(TempEvent());
    }

    IEnumerator TempEvent()
    {
        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);
        yield return new WaitForSeconds(1f);
        _isEnd = true;
    }

    public override bool IsEnd()
    {
        return _isEnd;
    }
}
