using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitMeleeAttackEvent : UnitEvent
{
    [SerializeField]
    private Vector3 offsetDistanceFromEnemy = new Vector3(1.0f, 0.0f, 0.0f);

    private bool    isEnd                   = false;
    private Transform _unitTransform;

    public override void Init()
    {
        isEnd = false;
        _unitTransform = _myUnit.transform;

    }

    public override void InvokeEvent()
    {
        StartCoroutine(Co_Attack());
    }

    private WaitForSeconds waitForSeconds00 = new WaitForSeconds(1.6f);
    private WaitForSeconds waitForSeconds01 = new WaitForSeconds(2.0f);
    private WaitForSeconds waitForSeconds02 = new WaitForSeconds(0.1f);

    public IEnumerator Co_Attack()
    {
        Vector3 _tempPosition               = _unitTransform.position;
        Vector3 _offsetDistanceFromEnemy    = offsetDistanceFromEnemy;

        // 적 유닛이 오른쪽에 있으면 
        if (_unitTransform.position.x < _myUnit.OpponentUnit.transform.position.x)
            _offsetDistanceFromEnemy *= -1;

        _unitTransform.DOMove(_myUnit.OpponentUnit.transform.position + _offsetDistanceFromEnemy, 1.5f);

        // 애니메이션 
        
        yield return waitForSeconds01;
        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);
        yield return waitForSeconds02;
        // 파티클 생성 

        _unitTransform.DOMove(_tempPosition, 1.5f);

        yield return waitForSeconds00;

        // 나의 턴 종료 
        isEnd = true;   

    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
