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
        // �ִϸ��̼�
        // ��ƼŬ ����

        _myUnit.UnitHP.AddValue(-int.MaxValue);

        isEnd = true;
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
