using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitBossAttackEvent : UnitEvent
{
    [SerializeField]
    private float moveSpeed;

    private readonly Vector3 mapCenterPos = new Vector3(6.0f, 0.0f, 0.0f);

    private bool isEnd = false;

    public override void Init()
    {
        isEnd = false;
    }

    public override void InvokeEvent()
    {
        StartCoroutine(Co_Attack());
    }

    private IEnumerator Co_Attack()
    {
        Vector3 targetPos = transform.position + mapCenterPos;
        Vector3 orginPos = transform.position;

        transform.DOMove(targetPos, moveSpeed);

        yield return new WaitForSeconds(moveSpeed + 0.2f);

        _myUnit.Animator.SetTrigger(_myUnit.HASH_ATTACK);

        yield return new WaitForSeconds(0.2f);

        transform.DOMove(orginPos, moveSpeed);

        yield return new WaitForSeconds(moveSpeed + 0.1f);

        isEnd = true;
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
