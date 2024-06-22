using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitMeleeAttackEvent : UnitEvent
{
    [SerializeField]
    private Vector3 offsetDistanceFromEnemy = new Vector3(1.0f, 0.0f, 0.0f);

    private bool    isEnd                   = false;

    public override void Init()
    {
        isEnd = false;
    }

    public override void InvokeEvent()
    {
        StartCoroutine(Co_Attack());
    }

    private WaitForSeconds waitForSeconds00 = new WaitForSeconds(1.6f);
    private WaitForSeconds waitForSeconds01 = new WaitForSeconds(2.0f);

    public IEnumerator Co_Attack()
    {
        Vector3 _tempPosition               = transform.position;
        Vector3 _offsetDistanceFromEnemy    = offsetDistanceFromEnemy;

        // 적 유닛이 오른쪽에 있으면 
        if (transform.position.x < _myUnit.OpponentUnit.transform.position.x)
            _offsetDistanceFromEnemy *= -1;

        transform.DOMove(_myUnit.OpponentUnit.transform.position + _offsetDistanceFromEnemy, 1.5f);

        // 애니메이션 
        // 파티클 생성 

        yield return waitForSeconds01;

        transform.DOMove(_tempPosition, 1.5f);

        yield return waitForSeconds00;

        // 나의 턴 종료 
        isEnd = true;   

    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
