using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitRangeAttackEvent : UnitEvent
{
    // # Fire
    [SerializeField]
    private Vector3     firePosOffet;
    [SerializeField]
    private GameObject  bulletPrefab;

    private bool        isEnd = false;

    public override void Init()
    {
        isEnd = false;
    }

    public override void InvokeEvent()
    {
        Vector3    targetDirection     = (_myUnit.OpponentUnit.transform.position - transform.position).normalized;
        GameObject clone               = Instantiate(bulletPrefab, _myUnit.transform.position + firePosOffet, Quaternion.identity);
        Bullet     bullet              = clone.GetComponent<Bullet>();
        // 애니메이션 재생 

        bullet.Initialized(this, _myUnit, targetDirection);
    }

    public override bool IsEnd()
    {
        return isEnd;
    }

    public void SetIsEnd(bool result)
    {
        isEnd = result;
    }
}
