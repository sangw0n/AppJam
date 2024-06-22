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
        int index = Random.Range(0, _commentary.Length);
        StartCoroutine(GameManager.Instance.Co_InputText(_commentary[index]));

        _myUnit.UnitHP.AddValue(-int.MaxValue);

        isEnd = true;
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
