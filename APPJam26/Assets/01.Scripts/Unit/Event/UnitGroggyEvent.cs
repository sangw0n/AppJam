using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroggyEvent : UnitEvent
{
    private bool isEnd;

    public override void Init()
    {
        isEnd = false;
    }

    public override void InvokeEvent()
    {
        // 애니메이션
        // 파티클 연출

        _myUnit.UnitHP.AddValue(-int.MaxValue);

        isEnd = true;
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
