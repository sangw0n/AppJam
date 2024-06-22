using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitJumpEvent : UnitEvent
{
    private bool _isEnd;

    public override void Init()
    {
        _isEnd = false;

    }

    WaitForSeconds _wait05 = new WaitForSeconds(0.5f);

    public override void InvokeEvent()
    {

        StartCoroutine(JumpCo());

    }

    IEnumerator JumpCo()
    {
        Transform unitTrm = _myUnit.transform;
        Transform opponentTrm = _myUnit.OpponentUnit.transform;

        Vector3 startPos = unitTrm.position;

        unitTrm.DOJump(opponentTrm.position, 3, 1, 0.5f).SetEase(Ease.InQuad);

        yield return _wait05;

        _myUnit.OpponentUnit.HitDamage(Mathf.RoundToInt(_myUnit.UnitData.Strength * 1.5f));
        unitTrm.DOMove(startPos, 0.2f);

        yield return _wait05;

        _isEnd = true;
    }

    public override bool IsEnd()
    {

        return _isEnd;

    }
}
