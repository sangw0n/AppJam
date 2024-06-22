using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitBossAttackEvent : UnitEvent
{
    [SerializeField]
    private float moveSpeed;

    private Transform mapCenterPos;

    private bool isEnd = false;

    public override void Init()
    {
        mapCenterPos = GameManager.Instance.CenterPos;

        isEnd = false;
    }

    public override void InvokeEvent()
    {
        int index = Random.Range(0, _commentary.Length);
        StartCoroutine(GameManager.Instance.Co_InputText(_commentary[index]));

        StartCoroutine(Co_Attack());
    }

    private IEnumerator Co_Attack()
    {
        Vector3 orginPos = transform.position;

        _myUnit.transform.DOMove(mapCenterPos.position, moveSpeed);

        yield return new WaitForSeconds(moveSpeed + 0.1f);

        _myUnit.Animator.SetTrigger(_myUnit.HASH_ATTACK);
        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength * 2);

        yield return new WaitForSeconds(0.4f);

        _myUnit.transform.DOMove(orginPos, moveSpeed);

        yield return new WaitForSeconds(moveSpeed + 0.1f);

        isEnd = true;
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
