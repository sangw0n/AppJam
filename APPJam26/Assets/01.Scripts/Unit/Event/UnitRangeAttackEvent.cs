using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitRangeAttackEvent : UnitEvent
{
    private bool isEnd = false;

    public override void Init()
    {
        isEnd = true;
    }

    public override void InvokeEvent()
    {

    }

    public override bool IsEnd()
    {
        return isEnd;
    }

}
